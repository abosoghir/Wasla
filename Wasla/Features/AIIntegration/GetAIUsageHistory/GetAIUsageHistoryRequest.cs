namespace Wasla.Features.AIIntegration.GetAIUsageHistory;

public record GetAIUsageHistoryRequest(int PageNumber = 1, int PageSize = 20) : IRequest<Result<GetAIUsageHistoryResponse>>;

public record GetAIUsageHistoryResponse(List<AIUsageDto> Items, PaginationResponse Pagination);

public record AIUsageDto(int Id, AIFeatureType FeatureType, int PointsCost, AIRequestStatus Status, string? ErrorMessage, DateTime UsedAt);
