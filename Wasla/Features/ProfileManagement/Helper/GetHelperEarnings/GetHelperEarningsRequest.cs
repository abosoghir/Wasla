namespace Wasla.Features.ProfileManagement.Helper.GetHelperEarnings;

public record GetHelperEarningsRequest(int PageNumber = 1, int PageSize = 20) : IRequest<Result<GetHelperEarningsResponse>>;

public record GetHelperEarningsResponse(
    decimal TotalEarnings,
    decimal TotalPlatformFees,
    decimal NetEarnings,
    decimal ThisMonthEarnings,
    int TotalCompletedPayments,
    List<EarningDto> RecentEarnings,
    PaginationResponse Pagination
);

public record EarningDto(
    int Id,
    decimal Amount,
    decimal PlatformFee,
    decimal NetAmount,
    string? Description,
    string? RelatedEntityType,
    int? RelatedEntityId,
    PaymentStatus Status,
    DateTime? PaidAt,
    DateTime CreatedOn
);
