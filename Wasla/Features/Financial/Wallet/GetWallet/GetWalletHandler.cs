namespace Wasla.Features.Financial.Wallet.GetWallet;

public class GetWalletHandler(
    IRepository<Wasla.Entities.Financial.Wallet> walletRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetWalletRequest, Result<GetWalletResponse>>
{
    private readonly IRepository<Wasla.Entities.Financial.Wallet> _walletRepo = walletRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetWalletResponse>> Handle(GetWalletRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var wallet = await _walletRepo.FindAsync(w => w.UserId == userId && w.IsActive, ct);
        if (wallet is null)
            return Result.Failure<GetWalletResponse>(FinancialErrors.WalletNotFound);

        return Result.Success(new GetWalletResponse(
            wallet.Id, wallet.Balance, wallet.TotalDeposited, wallet.TotalWithdrawn, wallet.Currency, wallet.IsActive
        ));
    }
}
