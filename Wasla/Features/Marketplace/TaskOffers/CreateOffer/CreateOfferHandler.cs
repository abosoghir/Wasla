namespace Wasla.Features.Marketplace.TaskOffers.CreateOffer;

public class CreateOfferHandler(
    IRepository<SeekerTask> taskRepo,
    IRepository<TaskOffer> offerRepo,
    IRepository<Helper> helperRepo,
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateOfferRequest, Result<CreateOfferResponse>>
{
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;
    private readonly IRepository<TaskOffer> _offerRepo = offerRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateOfferResponse>> Handle(CreateOfferRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null)
            return Result.Failure<CreateOfferResponse>(MarketplaceErrors.HelperNotFound);

        var task = await _taskRepo.GetByIdAsync(request.TaskId, ct);
        if (task is null)
            return Result.Failure<CreateOfferResponse>(MarketplaceErrors.TaskNotFound);

        if (task.Status != Common.Enums.TaskStatus.Open)
            return Result.Failure<CreateOfferResponse>(MarketplaceErrors.TaskNotOpen);

        // Don't let helper offer on their own task (if they are also a seeker)
        if (task.CreatedById == userId)
            return Result.Failure<CreateOfferResponse>(MarketplaceErrors.CannotOfferOwnTask);

        // Check for duplicate offer
        var alreadyOffered = await _offerRepo.AnyAsync(
            o => o.TaskId == request.TaskId && o.HelperId == helper.Id && o.Status == TaskOfferStatus.Pending, ct);
        if (alreadyOffered)
            return Result.Failure<CreateOfferResponse>(MarketplaceErrors.AlreadyOffered);

        var offer = new TaskOffer
        {
            TaskId = request.TaskId,
            HelperId = helper.Id,
            Message = request.Message,
            ProposedPrice = request.ProposedPrice,
            ProposedDurationDays = request.ProposedDurationDays,
            Status = TaskOfferStatus.Pending,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _offerRepo.AddAsync(offer, ct);

        // Notify the task owner
        var notification = new Notification
        {
            UserId = task.CreatedById,
            Type = NotificationType.NewOffer,
            Title = "New Offer Received",
            Body = $"A helper has submitted an offer for your task: \"{task.Title}\".",
            RelatedEntityType = "TaskOffer",
            RelatedEntityId = offer.Id,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _notificationRepo.AddAsync(notification, ct);
        await _offerRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateOfferResponse(offer.Id));
    }
}
