using Wasla.Common.Gamification;

namespace Wasla.Features.Marketplace.Tasks.CompleteTask;

public class CompleteTaskHandler(
    IRepository<SeekerTask> taskRepo,
    IRepository<Seeker> seekerRepo,
    IRepository<Helper> helperRepo,
    IRepository<Payment> paymentRepo,
    IRepository<Notification> notificationRepo,
    GamificationHelper gamificationHelper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CompleteTaskRequest, Result>
{
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<Payment> _paymentRepo = paymentRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly GamificationHelper _gamificationHelper = gamificationHelper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(CompleteTaskRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var task = await _taskRepo.GetByIdAsync(request.Id, ct);
        if (task is null)
            return Result.Failure(MarketplaceErrors.TaskNotFound);

       
        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null || task.SeekerId != seeker.Id)
            return Result.Failure(MarketplaceErrors.Unauthorized);

        if (task.Status != Common.Enums.TaskStatus.InProgress)
            return Result.Failure(MarketplaceErrors.TaskNotInProgress);

     
        task.Status = Common.Enums.TaskStatus.Completed;
        task.CompletedAt = DateTime.UtcNow;
        task.UpdatedById = userId;
        task.UpdatedOn = DateTime.UtcNow;

        
        task.FinalPrice = task.Budget ?? 0;

        
        if (task.FinalPrice > 0 && task.HelperId.HasValue)
        {
            var helper = await _helperRepo.GetByIdAsync(task.HelperId.Value, ct);
            if (helper is not null)
            {
                var platformFee = task.FinalPrice.Value * task.PlatformFee;

                var payment = new Payment
                {
                    PayerId = userId!,
                    PayeeId = helper.UserId,
                    Amount = task.FinalPrice.Value,
                    PlatformFee = platformFee,
                    Purpose = PaymentPurpose.Task,
                    RelatedEntityId = task.Id,
                    RelatedEntityType = "Task",
                    Method = PaymentMethod.Wallet,
                    Status = PaymentStatus.Completed,
                    PaidAt = DateTime.UtcNow,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                };
                // TODO: Implement Refund & Dispute handling in future versions

                await _paymentRepo.AddAsync(payment, ct);

                // Award +10 Points
                await _gamificationHelper.AddPointsAsync(
                    helper, task.PointsAwarded, PointTransactionType.Earned,
                    $"Task completed: {task.Title}", task.Id, "Task", userId!, ct);

                // Update Helper Stats
                helper.CompletedTasksCount++;
                helper.TotalEarnings += task.FinalPrice.Value - platformFee;
                await _helperRepo.UpdateAsync(helper, ct);

                // Check & Assign Badges
                await _gamificationHelper.CheckAndAwardBadgesAsync(helper, userId!, ct);

                // Create Notification for Helper
                var helperNotification = new Notification
                {
                    UserId = helper.UserId,
                    Type = NotificationType.TaskCompleted,
                    Title = "Task Completed!",
                    Body = $"The task \"{task.Title}\" has been completed. You earned {task.PointsAwarded} points!",
                    RelatedEntityType = "Task",
                    RelatedEntityId = task.Id,
                    Priority = NotificationPriority.High,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                };

                await _notificationRepo.AddAsync(helperNotification, ct);
            }
        }

        // Update seeker spent amount
        if (task.FinalPrice.HasValue)
            seeker.TotalAmountSpent += task.FinalPrice.Value;

        await _taskRepo.UpdateAsync(task, ct);
        await _seekerRepo.UpdateAsync(seeker, ct);
        await _taskRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
