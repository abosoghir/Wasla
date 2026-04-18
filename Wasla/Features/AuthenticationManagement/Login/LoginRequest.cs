using Wasla.Features.AuthenticationManagement;

namespace Wasla.Features.AuthenticationManagement.Login;

public record LoginRequest(
    string Email,
    string Password
) : IRequest<Result<AuthResponse>>;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}