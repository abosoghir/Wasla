using Microsoft.EntityFrameworkCore.Query;

namespace Wasla.Features.Notifications.MarkAllNotificationsAsRead;

public class MarkAllNotificationsAsReadHandler(
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<MarkAllNotificationsAsReadRequest, Result>
{
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(MarkAllNotificationsAsReadRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure(NotificationErrors.Unauthorized);

        await _notificationRepo.BulkUpdateWhereAsync(
            n => n.UserId == userId && !n.IsRead,
            s => s.SetProperty(n => n.IsRead, true)
                  .SetProperty(n => n.ReadAt, DateTime.UtcNow),
            ct
        );

        return Result.Success();
    }
}
