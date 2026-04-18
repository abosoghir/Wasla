namespace Wasla.Features.AuthenticationManagement.ResendConfirmationEmail;

public record ResendConfirmEmailRequest(
    string Email
) : IRequest<Result<ResendConfirmEmailResponse>>;

public class ResendConfirmEmailRequestValidator : AbstractValidator<ResendConfirmEmailRequest>
{
    public ResendConfirmEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}

