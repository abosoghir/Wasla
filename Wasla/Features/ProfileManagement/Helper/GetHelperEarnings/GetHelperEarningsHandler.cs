namespace Wasla.Features.ProfileManagement.Helper.GetHelperEarnings;

public class GetHelperEarningsHandler(
    IRepository<Payment> paymentRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetHelperEarningsRequest, Result<GetHelperEarningsResponse>>
{
    private readonly IRepository<Payment> _paymentRepo = paymentRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetHelperEarningsResponse>> Handle(GetHelperEarningsRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure<GetHelperEarningsResponse>(ProfileErrors.UserNotFound);

        var query = _paymentRepo.GetAll()
            .Where(p => p.PayeeId == userId && !p.IsDeleted)
            .OrderByDescending(p => p.CreatedOn);

        var completedPayments = await _paymentRepo.GetAll()
            .Where(p => p.PayeeId == userId && !p.IsDeleted && p.Status == PaymentStatus.Completed)
            .ToListAsync(ct);

        var totalEarnings = completedPayments.Sum(p => p.Amount);
        var totalFees = completedPayments.Sum(p => p.PlatformFee);
        var netEarnings = completedPayments.Sum(p => p.NetAmount);

        var firstOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var thisMonthEarnings = completedPayments
            .Where(p => p.PaidAt >= firstOfMonth)
            .Sum(p => p.NetAmount);

        var paginatedList = await PaginatedList<EarningDto>.CreateAsync(
            query.Select(p => new EarningDto(
                p.Id,
                p.Amount,
                p.PlatformFee,
                p.Amount - p.PlatformFee,
                $"Payment for {p.Purpose}",
                p.RelatedEntityType,
                p.RelatedEntityId,
                p.Status,
                p.PaidAt,
                p.CreatedOn
            )),
            request.PageNumber,
            request.PageSize,
            ct
        );

        return Result.Success(new GetHelperEarningsResponse(
            totalEarnings,
            totalFees,
            netEarnings,
            thisMonthEarnings,
            completedPayments.Count,
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
