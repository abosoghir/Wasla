namespace Wasla.Features.AuthenticationManagement.RevokeRefreshToken;

public record RevokeRefreshTokenRequest(
    string Token,
    string RefreshToken
) : IRequest<Result>;


public class RevokeRefreshTokenValidator : AbstractValidator<RevokeRefreshTokenRequest>
{
    public RevokeRefreshTokenValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}