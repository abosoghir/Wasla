namespace Wasla.Features.Notifications.MarkNotificationAsRead;

public record MarkNotificationAsReadRequest(int Id) : IRequest<Result>;
