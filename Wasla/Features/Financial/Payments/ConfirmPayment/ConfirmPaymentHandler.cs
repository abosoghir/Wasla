namespace Wasla.Features.Financial.Payments.ConfirmPayment;

public class ConfirmPaymentHandler(
    IRepository<Payment> paymentRepo,
    IRepository<Wasla.Entities.Financial.Wallet> walletRepo,
    IRepository<WalletTransaction> transactionRepo,
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<ConfirmPaymentRequest, Result>
{
    private readonly IRepository<Payment> _paymentRepo = paymentRepo;
    private readonly IRepository<Wasla.Entities.Financial.Wallet> _walletRepo = walletRepo;
    private readonly IRepository<WalletTransaction> _transactionRepo = transactionRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(ConfirmPaymentRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var payment = await _paymentRepo.GetByIdAsync(request.Id, ct);
        if (payment is null)
            return Result.Failure(FinancialErrors.PaymentNotFound);

        if (payment.Status != PaymentStatus.Pending)
            return Result.Failure(FinancialErrors.InvalidPaymentStatus);

        // Update payment
        payment.Status = PaymentStatus.Completed;
        payment.TransactionReference = request.TransactionReference; // Store the transaction reference from the payment gateway
        payment.PaidAt = DateTime.UtcNow;
        payment.UpdatedById = userId;
        payment.UpdatedOn = DateTime.UtcNow;
        await _paymentRepo.UpdateAsync(payment, ct);

        // Credit payee wallet if applicable
        if (payment.PayeeId is not null)
        {
            var payeeWallet = await _walletRepo.FindAsync(w => w.UserId == payment.PayeeId && w.IsActive, ct);
            if (payeeWallet is not null)
            {
                payeeWallet.Balance += payment.NetAmount;
                payeeWallet.TotalDeposited += payment.NetAmount;
                await _walletRepo.UpdateAsync(payeeWallet, ct);

                var walletTx = new WalletTransaction
                {
                    WalletId = payeeWallet.Id,
                    Amount = payment.NetAmount,
                    Type = TransactionType.Payment,
                    Status = TransactionStatus.Completed,
                    Description = $"Payment received for {payment.Purpose}",
                    RelatedEntityId = payment.RelatedEntityId,
                    RelatedEntityType = payment.RelatedEntityType,
                    BalanceAfter = payeeWallet.Balance,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                };
                await _transactionRepo.AddAsync(walletTx, ct);

                // Notify payee
                var notification = new Notification
                {
                    UserId = payment.PayeeId,
                    Type = NotificationType.PaymentReceived,
                    Title = "Payment Received",
                    Body = $"You received a payment of {payment.NetAmount:C}.",
                    RelatedEntityType = "Payment",
                    RelatedEntityId = payment.Id,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                };
                await _notificationRepo.AddAsync(notification, ct);
            }
        }

        await _paymentRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
