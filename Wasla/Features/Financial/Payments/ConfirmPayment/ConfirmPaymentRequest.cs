namespace Wasla.Features.Financial.Payments.ConfirmPayment;

public record ConfirmPaymentRequest(int Id, string? TransactionReference) : IRequest<Result>;
