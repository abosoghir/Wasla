namespace Wasla.Features.Marketplace.TaskOffers.RejectOffer;

public class RejectOfferHandler(
    IRepository<TaskOffer> offerRepo,
    IRepository<SeekerTask> taskRepo,
    IRepository<Seeker> seekerRepo,
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RejectOfferRequest, Result>
{
    private readonly IRepository<TaskOffer> _offerRepo = offerRepo;
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(RejectOfferRequest request, CancellationToken ct)
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

        offer.Status = TaskOfferStatus.Rejected;
        offer.UpdatedById = userId;
        offer.UpdatedOn = DateTime.UtcNow;
        await _offerRepo.UpdateAsync(offer, ct);

        // Notify the helper
        var notification = new Notification
        {
            UserId = offer.CreatedById,
            Type = NotificationType.OfferRejected,
            Title = "Offer Rejected",
            Body = $"Your offer for task \"{task.Title}\" has been rejected.",
            RelatedEntityType = "TaskOffer",
            RelatedEntityId = offer.Id,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _notificationRepo.AddAsync(notification, ct);
        await _offerRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
