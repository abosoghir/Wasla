namespace Wasla.Features.Notifications.DeleteNotification;

public record DeleteNotificationRequest(int Id) : IRequest<Result>;
