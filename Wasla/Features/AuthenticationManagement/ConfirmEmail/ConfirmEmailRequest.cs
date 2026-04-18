namespace Wasla.Features.AuthenticationManagement.ConfirmEmail;

public record ConfirmEmailRequest(
    string UserId, // we retrieve this form Register Endpoint response
    string Code   // the OTP code sent via email
) : IRequest<Result<AuthResponse>>;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Code)
            .NotEmpty();
    }
}