namespace Wasla.Features.Notifications.GetNotifications;

public class GetNotificationsHandler(
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetNotificationsRequest, Result<GetNotificationsResponse>>
{
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetNotificationsResponse>> Handle(GetNotificationsRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure<GetNotificationsResponse>(NotificationErrors.Unauthorized);

        var query = _notificationRepo.GetAll()
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedOn);

        if (request.UnreadOnly == true)
            query = (IOrderedQueryable<Notification>)query.Where(n => !n.IsRead);

        var unreadCount = await _notificationRepo.CountAsync(n => n.UserId == userId && !n.IsRead, ct);

        var paginatedList = await PaginatedList<NotificationDto>.CreateAsync(
            query.Select(n => new NotificationDto(
                n.Id, n.Type, n.Title, n.Body, n.ActionUrl, n.IsRead, n.CreatedOn
            )),
            request.PageNumber,
            request.PageSize,
            ct
        );

        var response = new GetNotificationsResponse(
            paginatedList.Items,
            new PaginationResponse
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = request.PageSize,
                TotalPages = paginatedList.TotalPages,
                HasNextPage = paginatedList.HasNextPage
            },
            unreadCount
        );

        return Result.Success(response);
    }
}
