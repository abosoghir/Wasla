namespace Wasla.Features.Financial.Wallet.GetTransactions;

public class GetTransactionsHandler(
    IRepository<Entities.Financial.Wallet> walletRepo,
    IRepository<WalletTransaction> transactionRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetTransactionsRequest, Result<GetTransactionsResponse>>
{
    private readonly IRepository<Entities.Financial.Wallet> _walletRepo = walletRepo;
    private readonly IRepository<WalletTransaction> _transactionRepo = transactionRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetTransactionsResponse>> Handle(GetTransactionsRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var wallet = await _walletRepo.FindAsync(w => w.UserId == userId && w.IsActive, ct);
        if (wallet is null)
            return Result.Failure<GetTransactionsResponse>(FinancialErrors.WalletNotFound);

        var query = _transactionRepo
            .FindAll(t => t.WalletId == wallet.Id)
            .OrderByDescending(t => t.CreatedOn);

        var paginatedList = await PaginatedList<WalletTransactionResponse>.CreateAsync(
            query.Select(t => new WalletTransactionResponse(
                t.Id, t.Amount, t.Type, t.Status, t.Description, t.BalanceAfter, t.CreatedOn
            )),
            request.PageNumber, request.PageSize, ct
        );

        return Result.Success(new GetTransactionsResponse(
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
