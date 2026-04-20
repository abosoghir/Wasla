namespace Wasla.Features.Notifications;

public static class NotificationErrors
{
    public static readonly Error NotificationNotFound =
        new("Notification.NotFound", "Notification not found.", StatusCodes.Status404NotFound);

    public static readonly Error Unauthorized =
        new("Notification.Unauthorized", "You are not authorized to access this notification.", StatusCodes.Status403Forbidden);
}
