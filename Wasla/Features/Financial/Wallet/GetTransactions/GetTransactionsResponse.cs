namespace Wasla.Features.Financial.Wallet.GetTransactions;

public record GetTransactionsResponse(List<WalletTransactionResponse> Items, PaginationResponse Pagination);

public record WalletTransactionResponse(int Id, decimal Amount, TransactionType Type, TransactionStatus Status, string? Description, decimal BalanceAfter, DateTime CreatedOn);

