using Wasla.Common.Email;
using Wasla.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Wasla.Features.AuthenticationManagement.ForgetPassword;

public class ForgetPasswordHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<ForgetPasswordHandler> logger,
    EmailHelper emailHelper) : IRequestHandler<ForgetPasswordRequest, Result<ForgetPasswordResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<ForgetPasswordHandler> _logger = logger;
    private readonly EmailHelper _emailHelper = emailHelper;

    public async Task<Result<ForgetPasswordResponse>> Handle(ForgetPasswordRequest request, CancellationToken ct)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Success<ForgetPasswordResponse>(default); // to mislead attackers 

        if (!user.EmailConfirmed)
            return Result.Failure<ForgetPasswordResponse>(UserErrors.EmailNotConfirmed);

        // Generate 6-digit OTP
        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        user.ResetPasswordCode = code;
        user.ResetPasswordCodeExpiration = DateTime.UtcNow.AddMinutes(60);

        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Reset Password OTP: {code}", code);

        await _emailHelper.SendResetPasswordEmail(user, code);

        return Result.Success(new ForgetPasswordResponse(user.Email!));
    }
}
