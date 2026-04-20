using Wasla.Common.Gamification;

namespace Wasla.Features.Marketplace.ProjectMilestones.ApproveMilestone;

public class ApproveMilestoneHandler(
    IRepository<ProjectMilestone> milestoneRepo,
    IRepository<Project> projectRepo,
    IRepository<Seeker> seekerRepo,
    IRepository<Helper> helperRepo,
    IRepository<Payment> paymentRepo,
    IRepository<Notification> notificationRepo,
    GamificationHelper gamificationHelper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<ApproveMilestoneRequest, Result>
{
    private readonly IRepository<ProjectMilestone> _milestoneRepo = milestoneRepo;
    private readonly IRepository<Project> _projectRepo = projectRepo;
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<Payment> _paymentRepo = paymentRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly GamificationHelper _gamificationHelper = gamificationHelper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(ApproveMilestoneRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var milestone = await _milestoneRepo.GetByIdAsync(request.Id, ct);
        if (milestone is null)
            return Result.Failure(MarketplaceErrors.MilestoneNotFound);

        if (milestone.Status != MilestoneStatus.Submitted)
            return Result.Failure(MarketplaceErrors.MilestoneNotSubmitted);

        var project = await _projectRepo.GetByIdAsync(milestone.ProjectId, ct);
        if (project is null)
            return Result.Failure(MarketplaceErrors.ProjectNotFound);

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null || project.SeekerId != seeker.Id)
            return Result.Failure(MarketplaceErrors.Unauthorized);

        // Approve milestone
        milestone.Status = MilestoneStatus.Approved;
        milestone.CompletedAt = DateTime.UtcNow;
        milestone.UpdatedById = userId;
        milestone.UpdatedOn = DateTime.UtcNow;
        await _milestoneRepo.UpdateAsync(milestone, ct);

        // Find the helper who worked on this project (from accepted offer)
        var acceptedOffer = await _projectRepo
            .GetAll()
            .Where(p => p.Id == project.Id)
            .SelectMany(p => p.Offers)
            .Where(o => o.Status == OfferStatus.Accepted)
            .FirstOrDefaultAsync(ct);

        if (acceptedOffer is not null)
        {
            var helper = await _helperRepo.GetByIdAsync(acceptedOffer.HelperId, ct);
            if (helper is not null)
            {
                // Create Payment
                var platformFee = milestone.Amount * 0.05m;
                var payment = new Payment
                {
                    PayerId = userId!,
                    PayeeId = helper.UserId,
                    Amount = milestone.Amount,
                    PlatformFee = platformFee,
                    Purpose = PaymentPurpose.Project,
                    RelatedEntityId = milestone.Id,
                    RelatedEntityType = "ProjectMilestone",
                    Method = PaymentMethod.Wallet,
                    Status = PaymentStatus.Completed,
                    PaidAt = DateTime.UtcNow,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                };
                // TODO: Implement Refund & Dispute handling in future versions
                await _paymentRepo.AddAsync(payment, ct);

                // Award +20 Points
                await _gamificationHelper.AddPointsAsync(
                    helper, 20, PointTransactionType.Earned,
                    $"Milestone approved: {milestone.Title}", milestone.Id, "ProjectMilestone", userId!, ct);

                // Update Helper Stats
                helper.TotalEarnings += milestone.Amount - platformFee;
                await _helperRepo.UpdateAsync(helper, ct);

                // Check & Assign Badges
                await _gamificationHelper.CheckAndAwardBadgesAsync(helper, userId!, ct);

                // Notify helper
                var notification = new Notification
                {
                    UserId = helper.UserId,
                    Type = NotificationType.MilestoneApproved,
                    Title = "Milestone Approved!",
                    Body = $"The milestone \"{milestone.Title}\" has been approved. You earned 20 points!",
                    RelatedEntityType = "ProjectMilestone",
                    RelatedEntityId = milestone.Id,
                    Priority = NotificationPriority.High,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                };
                await _notificationRepo.AddAsync(notification, ct);
            }
        }

        await _milestoneRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
