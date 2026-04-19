namespace Wasla.Features.Communication.Sessions.ConfirmSession;

public class ConfirmSessionHandler(
    IRepository<Session> sessionRepo,
    IRepository<Helper> helperRepo,
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<ConfirmSessionRequest, Result>
{
    private readonly IRepository<Session> _sessionRepo = sessionRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(ConfirmSessionRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var session = await _sessionRepo.GetByIdAsync(request.Id, ct);
        if (session is null)
            return Result.Failure(CommunicationErrors.SessionNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null || session.HelperId != helper.Id)
            return Result.Failure(CommunicationErrors.Unauthorized);

        if (session.Status != SessionStatus.Pending)
            return Result.Failure(CommunicationErrors.InvalidSessionStatus);

        session.Status = SessionStatus.Confirmed;
        session.MeetingLink = request.MeetingLink;
        session.UpdatedById = userId;
        session.UpdatedOn = DateTime.UtcNow;

        await _sessionRepo.UpdateAsync(session, ct);

        var notification = new Notification
        {
            UserId = session.CreatedById,
            Type = NotificationType.SessionConfirmed,
            Title = "Session Confirmed",
            Body = $"Your consultation session has been confirmed for {session.ScheduledAt:g}.",
            RelatedEntityType = "Session",
            RelatedEntityId = session.Id,
            Priority = NotificationPriority.High,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _notificationRepo.AddAsync(notification, ct);
        await _sessionRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
