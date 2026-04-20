namespace Wasla.Features.Marketplace.TaskOffers.GetMyOffers;

public class GetMyOffersHandler(
    IRepository<TaskOffer> offerRepo,
    IRepository<Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetMyOffersRequest, Result<GetMyOffersResponse>>
{
    private readonly IRepository<TaskOffer> _offerRepo = offerRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetMyOffersResponse>> Handle(GetMyOffersRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure<GetMyOffersResponse>(MarketplaceErrors.Unauthorized);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper == null)
            return Result.Failure<GetMyOffersResponse>(MarketplaceErrors.HelperNotFound);

        var query = _offerRepo.GetAll()
            .Include(o => o.Task)
            .Where(o => o.HelperId == helper.Id && !o.IsDeleted);

        if (request.Status.HasValue)
            query = query.Where(o => o.Status == request.Status.Value);

        query = query.OrderByDescending(o => o.CreatedOn);

        var paginatedList = await PaginatedList<MyOfferResponse>.CreateAsync(
            query.Select(o => new MyOfferResponse(
                o.Id, o.TaskId, o.Task.Title, o.Task.Category,
                o.Message, o.ProposedPrice, o.ProposedDurationDays,
                o.Status, o.CreatedOn
            )),
            request.PageNumber,
            request.PageSize,
            ct
        );

        return Result.Success(new GetMyOffersResponse(
            paginatedList.Items,
            new PaginationResponse
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = request.PageSize,
                TotalPages = paginatedList.TotalPages,
                HasNextPage = paginatedList.HasNextPage
            }
        ));
    }
}
