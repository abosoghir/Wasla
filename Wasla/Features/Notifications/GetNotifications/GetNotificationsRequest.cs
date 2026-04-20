namespace Wasla.Features.Notifications.GetNotifications;

public record GetNotificationsRequest(bool? UnreadOnly, int PageNumber = 1, int PageSize = 20)
    : IRequest<Result<GetNotificationsResponse>>;

public record GetNotificationsResponse(List<NotificationDto> Items, PaginationResponse Pagination, int UnreadCount);

public record NotificationDto(
    int Id,
    NotificationType Type,
    string Title,
    string Body,
    string? ActionUrl,
    bool IsRead,
    DateTime CreatedOn
);
