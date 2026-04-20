namespace Wasla.Features.Financial.Wallet.MockDepositFunds;

public record MockDepositFundsRequest(decimal Amount, PaymentMethod Method) : IRequest<Result>;

public class MockDepositFundsRequestValidator : AbstractValidator<MockDepositFundsRequest>
{
    public MockDepositFundsRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Deposit amount must be greater than zero.");
        RuleFor(x => x.Method).IsInEnum();
    }
}
