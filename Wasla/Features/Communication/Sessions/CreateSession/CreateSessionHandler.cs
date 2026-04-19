namespace Wasla.Features.Communication.Sessions.CreateSession;

public class CreateSessionHandler(
    IRepository<Seeker> seekerRepo,
    IRepository<Session> sessionRepo,
    IRepository<Notification> notificationRepo,
    IRepository<Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateSessionRequest, Result<CreateSessionResponse>>
{
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<Session> _sessionRepo = sessionRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateSessionResponse>> Handle(CreateSessionRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null)
            return Result.Failure<CreateSessionResponse>(CommunicationErrors.SeekerNotFound);

        var helper = await _helperRepo.GetByIdAsync(request.HelperId, ct);
        if (helper is null)
            return Result.Failure<CreateSessionResponse>(CommunicationErrors.HelperNotFound);

        var session = new Session
        {
            SeekerId = seeker.Id,
            HelperId = request.HelperId,
            ScheduledAt = request.ScheduledAt,
            DurationMinutes = request.DurationMinutes,
            TotalPrice = request.TotalPrice,
            Status = SessionStatus.Pending,
            MeetingPlatform = request.MeetingPlatform,
            Notes = request.Notes,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _sessionRepo.AddAsync(session, ct);

        seeker.TotalSessionsBooked++;
        await _seekerRepo.UpdateAsync(seeker, ct);

        var notification = new Notification
        {
            UserId = helper.UserId,
            Type = NotificationType.SessionConfirmed,
            Title = "New Session Request",
            Body = $"A seeker has requested a consultation session on {request.ScheduledAt:g}.",
            RelatedEntityType = "Session",
            RelatedEntityId = session.Id,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _notificationRepo.AddAsync(notification, ct);
        await _sessionRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateSessionResponse(session.Id));
    }
}
