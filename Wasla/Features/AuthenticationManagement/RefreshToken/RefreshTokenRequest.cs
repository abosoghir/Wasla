namespace Wasla.Features.AuthenticationManagement.RefreshToken;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
) : IRequest<Result<AuthResponse>>;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}