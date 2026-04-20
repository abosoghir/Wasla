namespace Wasla.Features.Marketplace.TaskOffers.AcceptOffer;

public class AcceptOfferHandler(
    IRepository<TaskOffer> offerRepo,
    IRepository<SeekerTask> taskRepo,
    IRepository<Seeker> seekerRepo,
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AcceptOfferRequest, Result>
{
    private readonly IRepository<TaskOffer> _offerRepo = offerRepo;
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(AcceptOfferRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var offer = await _offerRepo.GetByIdAsync(request.Id, ct);
        if (offer is null)
            return Result.Failure(MarketplaceErrors.OfferNotFound);

        if (offer.Status != TaskOfferStatus.Pending)
            return Result.Failure(MarketplaceErrors.OfferNotPending);

        var task = await _taskRepo.GetByIdAsync(offer.TaskId, ct);
        if (task is null)
            return Result.Failure(MarketplaceErrors.TaskNotFound);

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null || task.SeekerId != seeker.Id)
            return Result.Failure(MarketplaceErrors.Unauthorized);

        if (task.Status != Common.Enums.TaskStatus.Open)
            return Result.Failure(MarketplaceErrors.TaskNotOpen);

        // Accept this offer
        offer.Status = TaskOfferStatus.Accepted;
        offer.AcceptedAt = DateTime.UtcNow;
        offer.UpdatedById = userId;
        offer.UpdatedOn = DateTime.UtcNow;
        await _offerRepo.UpdateAsync(offer, ct);

        // Assign helper and set task to InProgress
        task.HelperId = offer.HelperId;
        task.Status = Common.Enums.TaskStatus.InProgress;
        task.Budget = offer.ProposedPrice;
        task.UpdatedById = userId;
        task.UpdatedOn = DateTime.UtcNow;
        await _taskRepo.UpdateAsync(task, ct);

        // Reject other pending offers
        var otherOffers = await _offerRepo
            .FindAll(o => o.TaskId == task.Id && o.Id != offer.Id && o.Status == TaskOfferStatus.Pending)
            .ToListAsync(ct);

        foreach (var other in otherOffers)
        {
            other.Status = TaskOfferStatus.Rejected;
            other.UpdatedById = userId;
            other.UpdatedOn = DateTime.UtcNow;
            await _offerRepo.UpdateAsync(other, ct);
        }

        // Notify the accepted helper
        var notification = new Notification
        {
            UserId = offer.CreatedById,
            Type = NotificationType.OfferAccepted,
            Title = "Offer Accepted!",
            Body = $"Your offer for task \"{task.Title}\" has been accepted.",
            RelatedEntityType = "Task",
            RelatedEntityId = task.Id,
            Priority = NotificationPriority.High,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _notificationRepo.AddAsync(notification, ct);
        await _offerRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
