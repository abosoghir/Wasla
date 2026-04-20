namespace Wasla.Features.Financial.Payments.CreatePayment;

public record CreatePaymentRequest(string? PayeeId, decimal Amount, PaymentPurpose Purpose, int? RelatedEntityId, string? RelatedEntityType, PaymentMethod Method)
    : IRequest<Result<CreatePaymentResponse>>;


public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Purpose).IsInEnum();
        RuleFor(x => x.Method).IsInEnum();
    }
}
