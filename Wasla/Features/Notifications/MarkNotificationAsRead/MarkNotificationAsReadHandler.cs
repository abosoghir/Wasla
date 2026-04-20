namespace Wasla.Features.Notifications.MarkNotificationAsRead;

public class MarkNotificationAsReadHandler(
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<MarkNotificationAsReadRequest, Result>
{
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(MarkNotificationAsReadRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure(NotificationErrors.Unauthorized);

        var notification = await _notificationRepo.FindAsync(n => n.Id == request.Id, ct);
        if (notification == null)
            return Result.Failure(NotificationErrors.NotificationNotFound);

        if (notification.UserId != userId)
            return Result.Failure(NotificationErrors.Unauthorized);

        notification.IsRead = true;
        notification.ReadAt = DateTime.UtcNow;

        await _notificationRepo.UpdateAsync(notification, ct);
        await _notificationRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
