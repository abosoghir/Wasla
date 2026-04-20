namespace Wasla.Features.Financial.Wallet.GetTransactions;

public record GetTransactionsRequest(int PageNumber = 1, int PageSize = 20) : IRequest<Result<GetTransactionsResponse>>;

