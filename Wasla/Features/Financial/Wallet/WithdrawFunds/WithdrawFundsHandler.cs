using WalletEntity = Wasla.Entities.Financial.Wallet;

namespace Wasla.Features.Financial.Wallet.WithdrawFunds;

public class WithdrawFundsHandler(
    IRepository<WalletEntity> walletRepo,
    IRepository<WalletTransaction> transactionRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<WithdrawFundsRequest, Result>
{
    private readonly IRepository<WalletEntity> _walletRepo = walletRepo;
    private readonly IRepository<WalletTransaction> _transactionRepo = transactionRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(WithdrawFundsRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure(FinancialErrors.Unauthorized);

        var wallet = await _walletRepo.FindAsync(w => w.UserId == userId && w.IsActive, ct);
        if (wallet == null)
            return Result.Failure(FinancialErrors.WalletNotFound);

        if (wallet.Balance < request.Amount)
            return Result.Failure(FinancialErrors.InsufficientBalance);

        wallet.Balance -= request.Amount;
        wallet.TotalWithdrawn += request.Amount;

        var transaction = new WalletTransaction
        {
            WalletId = wallet.Id,
            Amount = request.Amount,
            Type = TransactionType.Withdrawal,
            Status = TransactionStatus.Pending,
            Description = $"Withdrawal via {request.Method}",
            BalanceAfter = wallet.Balance
        };

        await _transactionRepo.AddAsync(transaction, ct);
        await _walletRepo.UpdateAsync(wallet, ct);
        await _walletRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
