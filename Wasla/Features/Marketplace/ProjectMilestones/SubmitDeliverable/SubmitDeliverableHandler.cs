namespace Wasla.Features.Marketplace.ProjectMilestones.SubmitDeliverable;

public class SubmitDeliverableHandler(
    IRepository<ProjectMilestone> milestoneRepo,
    IRepository<MilestoneDeliverable> deliverableRepo,
    IRepository<Notification> notificationRepo,
    IRepository<Project> projectRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SubmitDeliverableRequest, Result<SubmitDeliverableResponse>>
{
    private readonly IRepository<ProjectMilestone> _milestoneRepo = milestoneRepo;
    private readonly IRepository<MilestoneDeliverable> _deliverableRepo = deliverableRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IRepository<Project> _projectRepo = projectRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<SubmitDeliverableResponse>> Handle(SubmitDeliverableRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var milestone = await _milestoneRepo.GetByIdAsync(request.MilestoneId, ct);
        if (milestone is null)
            return Result.Failure<SubmitDeliverableResponse>(MarketplaceErrors.MilestoneNotFound);

        if (milestone.Status != MilestoneStatus.Pending && milestone.Status != MilestoneStatus.InProgress)
            return Result.Failure<SubmitDeliverableResponse>(MarketplaceErrors.MilestoneNotPending);

        var deliverable = new MilestoneDeliverable
        {
            MilestoneId = milestone.Id,
            FileName = request.FileName,
            FileUrl = request.FileUrl,
            FileType = request.FileType,
            FileSize = request.FileSize,
            Description = request.Description,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _deliverableRepo.AddAsync(deliverable, ct);

        milestone.Status = MilestoneStatus.Submitted;
        milestone.UpdatedById = userId;
        milestone.UpdatedOn = DateTime.UtcNow;
        await _milestoneRepo.UpdateAsync(milestone, ct);

        // Notify the project owner
        var project = await _projectRepo.GetByIdAsync(milestone.ProjectId, ct);
        if (project is not null)
        {
            var notification = new Notification
            {
                UserId = project.CreatedById,
                Type = NotificationType.MilestoneSubmitted,
                Title = "Milestone Deliverable Submitted",
                Body = $"A deliverable has been submitted for milestone \"{milestone.Title}\".",
                RelatedEntityType = "ProjectMilestone",
                RelatedEntityId = milestone.Id,
                CreatedById = userId!,
                CreatedOn = DateTime.UtcNow
            };
            await _notificationRepo.AddAsync(notification, ct);
        }

        await _deliverableRepo.SaveChangesAsync(ct);

        return Result.Success(new SubmitDeliverableResponse(deliverable.Id));
    }
}
