namespace Wasla.Features.Notifications.DeleteNotification;

public class DeleteNotificationHandler(
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<DeleteNotificationRequest, Result>
{
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(DeleteNotificationRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure(NotificationErrors.Unauthorized);

        var notification = await _notificationRepo.FindAsync(n => n.Id == request.Id, ct);
        if (notification == null)
            return Result.Failure(NotificationErrors.NotificationNotFound);

        if (notification.UserId != userId)
            return Result.Failure(NotificationErrors.Unauthorized);

        await _notificationRepo.Delete(notification);
        await _notificationRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
