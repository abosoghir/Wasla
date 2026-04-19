using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Wasla.Features.AuthenticationManagement.ConfirmEmail;

public class ConfirmEmailHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider)
    : IRequestHandler<ConfirmEmailRequest, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly int _refreshTokenExpiryDays = 30;

    public async Task<Result<AuthResponse>> Handle(ConfirmEmailRequest request, CancellationToken ct)
    {
        if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCode);

        if (user.EmailConfirmed)
            return Result.Failure<AuthResponse>(UserErrors.DuplicatedConfirmation);

        if (user.EmailConfirmationCode != request.Code ||
             user.EmailConfirmationCodeExpiration < DateTime.UtcNow)
        {
            return Result.Failure<AuthResponse>(UserErrors.InvalidCode);
        }

        user.EmailConfirmed = true;
        user.EmailConfirmationCode = null;
        user.EmailConfirmationCodeExpiration = null;
        await _userManager.UpdateAsync(user);

        // Add Role
        //await _userManager.AddToRoleAsync(user, DefaultRoles.Helper);

        // Generate JWT + JTI
        // Get Roles
        var roles = await _userManager.GetRolesAsync(user);

        var (token, expiresIn, jti) =
            _jwtProvider.GenerateToken(user, roles);

        // Generate Refresh Token
        var refreshToken = Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));

        var refreshTokenExpiration =
            DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        // Bind refresh token to JWT
        user.RefreshTokens.Add(new Entities.Identity.RefreshToken
        {
            Token = refreshToken,
            JwtId = jti,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);

        // Create AuthResponse
        var response = new AuthResponse(
            Id: user.Id,
            Email: user.Email!,
            Name: user.Name,
            PhoneNumber: user.PhoneNumber,
            Roles: roles,
            Token: token,
            ExpirseIn: expiresIn,
            RefreshToken: refreshToken,
            RefreshTokenExpiration: refreshTokenExpiration
        );

        return Result.Success(response);
    }
}
