using Wasla.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Wasla.Features.AuthenticationManagement.RevokeRefreshToken;

public class RevokeRefreshTokenHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider)
    : IRequestHandler<RevokeRefreshTokenRequest, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<Result> Handle(
        RevokeRefreshTokenRequest request,
        CancellationToken ct)
    {
        // Validate JWT signature ONLY (ignore expiration)
        var userId = _jwtProvider.ValidateExpiredToken(request.Token);

        if (userId is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        // Find active refresh token
        var refreshToken = user.RefreshTokens
            .SingleOrDefault(rt =>
                rt.Token == request.RefreshToken &&
                rt.IsActive);

        if (refreshToken is null)
            return Result.Failure(UserErrors.InvalidRefreshToken);

        // Revoke refresh token (logout)
        refreshToken.RevokedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }
}
