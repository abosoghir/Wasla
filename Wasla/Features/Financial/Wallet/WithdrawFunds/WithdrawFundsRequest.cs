namespace Wasla.Features.Financial.Wallet.WithdrawFunds;

public record WithdrawFundsRequest(decimal Amount, PaymentMethod Method) : IRequest<Result>;

public class WithdrawFundsRequestValidator : AbstractValidator<WithdrawFundsRequest>
{
    public WithdrawFundsRequestValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Withdrawal amount must be greater than zero.");
        RuleFor(x => x.Method).IsInEnum();
    }
}
