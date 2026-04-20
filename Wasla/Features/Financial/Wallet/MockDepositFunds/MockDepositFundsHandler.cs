using WalletEntity = Wasla.Entities.Financial.Wallet;

namespace Wasla.Features.Financial.Wallet.MockDepositFunds;

public class MockDepositFundsHandler(
    IRepository<WalletEntity> walletRepo,
    IRepository<WalletTransaction> transactionRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<MockDepositFundsRequest, Result>
{
    private readonly IRepository<WalletEntity> _walletRepo = walletRepo;
    private readonly IRepository<WalletTransaction> _transactionRepo = transactionRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(MockDepositFundsRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure(FinancialErrors.Unauthorized);

        var wallet = await _walletRepo.FindAsync(w => w.UserId == userId && w.IsActive, ct);
        if (wallet == null)
        {
            // Auto-create wallet if it doesn't exist for mocking purposes
            wallet = new WalletEntity
            {
                UserId = userId,
                Balance = 0,
                TotalDeposited = 0,
                TotalWithdrawn = 0,
                Currency = CurrencyType.EGP,
                IsActive = true
            };
            await _walletRepo.AddAsync(wallet, ct);
            await _walletRepo.SaveChangesAsync(ct);
        }

        // Mock successful payment integration
        wallet.Balance += request.Amount;
        wallet.TotalDeposited += request.Amount;

        var transaction = new WalletTransaction
        {
            WalletId = wallet.Id,
            Amount = request.Amount,
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Completed, // Mocked as immediately completed
            Description = $"Deposit via {request.Method} (Mock)",
            BalanceAfter = wallet.Balance
        };

        await _transactionRepo.AddAsync(transaction, ct);
        await _walletRepo.UpdateAsync(wallet, ct);
        await _walletRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
