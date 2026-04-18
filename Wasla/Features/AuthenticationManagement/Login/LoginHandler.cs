using Wasla.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Wasla.Features.AuthenticationManagement.Login;

public class LoginHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider,
    IRepository<ApplicationUser> repository) : IRequestHandler<LoginRequest, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IRepository<ApplicationUser> _repository = repository;
    private readonly int _refreshTokenExpiryDays = 30;

    public async Task<Result<AuthResponse>> Handle(LoginRequest request, CancellationToken ct)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);

            // Generate Access Token + JTI
            var (token, expiresIn, jti) =
                _jwtProvider.GenerateToken(user, roles);

            // Generate Refresh Token
            var refreshToken = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            var refreshTokenExpiration =
                DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            // Bind refresh token to this JWT
            user.RefreshTokens.Add(new Entities.Identity.RefreshToken
            {
                Token = refreshToken,
                JwtId = jti,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            return Result.Success(new AuthResponse(
                user.Id,
                user.Email!,
                user.Name,
                user.PhoneNumber,
                roles,
                token,
                expiresIn,
                refreshToken,
                refreshTokenExpiration
            ));
        }


        // the password is not valid or the email is not confirmed
        return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials);
    }

}
