namespace Wasla.Features.Financial.Wallet.GetWallet;

public record GetWalletResponse(int Id, decimal Balance, decimal TotalDeposited, decimal TotalWithdrawn, CurrencyType Currency, bool IsActive);

