using Wasla.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Wasla.Features.AuthenticationManagement.RefreshToken;

public class RefreshTokenHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider)
    : IRequestHandler<RefreshTokenRequest, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private const int RefreshTokenExpiryDays = 30;

    public async Task<Result<AuthResponse>> Handle(
        RefreshTokenRequest request,
        CancellationToken ct)
    {
        // Validate JWT WITHOUT checking expiration
        var userId = _jwtProvider.ValidateExpiredToken(request.Token);

        if (userId is null)
            return Result.Failure<AuthResponse>(
                UserErrors.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<AuthResponse>(
                UserErrors.InvalidJwtToken);

        // Find active refresh token
        var refreshToken = user.RefreshTokens
            .SingleOrDefault(rt =>
                rt.Token == request.RefreshToken);

        if (refreshToken is null || !refreshToken.IsActive)
            return Result.Failure<AuthResponse>(
                UserErrors.InvalidRefreshToken);

        // Revoke old refresh token (rotation)
        refreshToken.RevokedOn = DateTime.UtcNow;

        // Get roles
        var roles = await _userManager.GetRolesAsync(user);

        // Generate new access token
        var (newJwt, expiresIn, jti) =
            _jwtProvider.GenerateToken(user, roles);

        // Generate new refresh token
        var newRefreshToken = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));

        user.RefreshTokens.Add(new Entities.Identity.RefreshToken
        {
            Token = newRefreshToken,
            JwtId = jti,
            ExpiresOn = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays)
        });

        await _userManager.UpdateAsync(user);

        return Result.Success(new AuthResponse(
            user.Id,
            user.Email!,
            user.Name,
            user.PhoneNumber,
            roles,
            newJwt,
            expiresIn,
            newRefreshToken,
            DateTime.UtcNow.AddDays(RefreshTokenExpiryDays)
        ));
    }
}
