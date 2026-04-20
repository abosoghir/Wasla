namespace Wasla.Features.AIIntegration.GetAIUsageHistory;

public class GetAIUsageHistoryHandler(
    IRepository<AIUsage> aiUsageRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetAIUsageHistoryRequest, Result<GetAIUsageHistoryResponse>>
{
    private readonly IRepository<AIUsage> _aiUsageRepo = aiUsageRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetAIUsageHistoryResponse>> Handle(GetAIUsageHistoryRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var query = _aiUsageRepo
            .FindAll(u => u.UserId == userId)
            .OrderByDescending(u => u.UsedAt);

        var paginatedList = await PaginatedList<AIUsageDto>.CreateAsync(
            query.Select(u => new AIUsageDto(
                u.Id, u.FeatureType, u.PointsCost, u.Status, u.ErrorMessage, u.UsedAt
            )),
            request.PageNumber, request.PageSize, ct
        );

        return Result.Success(new GetAIUsageHistoryResponse(
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
