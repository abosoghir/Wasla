using Wasla.Common.Gamification;

namespace Wasla.Features.Communication.Sessions.CompleteSession;

public class CompleteSessionHandler(
    IRepository<Session> sessionRepo,
    IRepository<Helper> helperRepo,
    IRepository<Payment> paymentRepo,
    IRepository<Notification> notificationRepo,
    GamificationHelper gamificationHelper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CompleteSessionRequest, Result>
{
    private readonly IRepository<Session> _sessionRepo = sessionRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<Payment> _paymentRepo = paymentRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly GamificationHelper _gamificationHelper = gamificationHelper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(CompleteSessionRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var session = await _sessionRepo.GetByIdAsync(request.Id, ct);
        if (session is null)
            return Result.Failure(CommunicationErrors.SessionNotFound);

        if (session.Status != SessionStatus.Confirmed && session.Status != SessionStatus.InProgress)
            return Result.Failure(CommunicationErrors.InvalidSessionStatus);

        // 1. Update Session Status
        session.Status = SessionStatus.Completed;
        session.UpdatedById = userId;
        session.UpdatedOn = DateTime.UtcNow;
        await _sessionRepo.UpdateAsync(session, ct);

        var helper = await _helperRepo.GetByIdAsync(session.HelperId, ct);
        if (helper is not null)
        {
            // 2. Create Payment
            if (session.TotalPrice > 0)
            {
                var platformFee = session.TotalPrice * 0.05m;
                var payment = new Payment
                {
                    PayerId = session.CreatedById,
                    PayeeId = helper.UserId,
                    Amount = session.TotalPrice,
                    PlatformFee = platformFee,
                    Purpose = PaymentPurpose.Session,
                    RelatedEntityId = session.Id,
                    RelatedEntityType = "Session",
                    Method = PaymentMethod.Wallet,
                    Status = PaymentStatus.Completed,
                    PaidAt = DateTime.UtcNow,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                };
                // TODO: Implement Refund & Dispute handling in future versions
                await _paymentRepo.AddAsync(payment, ct);

                helper.TotalEarnings += session.TotalPrice - platformFee;
            }

            // 3. Award +8 Points
            await _gamificationHelper.AddPointsAsync(
                helper, 8, PointTransactionType.Earned,
                "Session completed", session.Id, "Session", userId!, ct);

            // 4. Update Helper Stats
            helper.CompletedSessionsCount++;
            await _helperRepo.UpdateAsync(helper, ct);

            // 5. Check & Assign Badges
            await _gamificationHelper.CheckAndAwardBadgesAsync(helper, userId!, ct);

            // 6. Create Notification
            var notification = new Notification
            {
                UserId = helper.UserId,
                Type = NotificationType.SessionCompleted,
                Title = "Session Completed!",
                Body = "Your consultation session has been completed. You earned 8 points!",
                RelatedEntityType = "Session",
                RelatedEntityId = session.Id,
                Priority = NotificationPriority.High,
                CreatedById = userId!,
                CreatedOn = DateTime.UtcNow
            };
            await _notificationRepo.AddAsync(notification, ct);
        }

        await _sessionRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
