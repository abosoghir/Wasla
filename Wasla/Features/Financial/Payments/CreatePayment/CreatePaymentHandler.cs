namespace Wasla.Features.Financial.Payments.CreatePayment;

public class CreatePaymentHandler(
    IRepository<Payment> paymentRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreatePaymentRequest, Result<CreatePaymentResponse>>
{
    private readonly IRepository<Payment> _paymentRepo = paymentRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreatePaymentResponse>> Handle(CreatePaymentRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var platformFee = request.Amount * 0.05m;

        var payment = new Payment
        {
            PayerId = userId!,
            PayeeId = request.PayeeId,
            Amount = request.Amount,
            PlatformFee = platformFee,
            Purpose = request.Purpose,
            RelatedEntityId = request.RelatedEntityId,
            RelatedEntityType = request.RelatedEntityType,
            Method = request.Method,
            Status = PaymentStatus.Pending,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };
        // TODO: Implement Refund & Dispute handling in future versions

        await _paymentRepo.AddAsync(payment, ct);

        await _paymentRepo.SaveChangesAsync(ct);

        return Result.Success(new CreatePaymentResponse(payment.Id));
    }
}
