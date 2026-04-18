using Wasla.Common.Email;
using Wasla.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Wasla.Features.AuthenticationManagement.ResendConfirmationEmail;

public class ResendConfirmEmailHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<ResendConfirmEmailHandler> logger,
    EmailHelper emailHelper) : IRequestHandler<ResendConfirmEmailRequest, Result<ResendConfirmEmailResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<ResendConfirmEmailHandler> _logger = logger;
    private readonly EmailHelper _emailHelper = emailHelper;

    public async Task<Result<ResendConfirmEmailResponse>> Handle(ResendConfirmEmailRequest request, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Success(new ResendConfirmEmailResponse(null));

        if (user.EmailConfirmed)
            return Result.Failure<ResendConfirmEmailResponse>(UserErrors.DuplicatedConfirmation);

        // Generate 6-digit OTP
        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        user.EmailConfirmationCode = code;
        user.EmailConfirmationCodeExpiration = DateTime.UtcNow.AddMinutes(60);
        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Confirmation OTP: {code}", code);

        // Send OTP via email
        await _emailHelper.SendConfirmationEmail(user, code);

        return Result.Success(new ResendConfirmEmailResponse(user.Id));
    }
}
