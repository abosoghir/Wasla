using Wasla.Common.Email;
using Wasla.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Wasla.Features.AuthenticationManagement.Register;

public class RegisterHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<RegisterHandler> logger,
    EmailHelper emailHelper) : IRequestHandler<RegisterRequest, Result<RegisterResponse>>

{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<RegisterHandler> _logger = logger;
    private readonly EmailHelper _emailHelper = emailHelper;
    public async Task<Result<RegisterResponse>> Handle(RegisterRequest request, CancellationToken ct)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == request.Email, ct))
            return Result.Failure<RegisterResponse>(UserErrors.DuplicatedEmail);


        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();

            return Result.Failure<RegisterResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        // Generate 6-digit OTP
        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
        user.EmailConfirmationCode = code;
        user.EmailConfirmationCodeExpiration = DateTime.UtcNow.AddMinutes(60);
        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Confirmation OTP: {code}", code);

        // Send OTP via email
        await _emailHelper.SendConfirmationEmail(user, code);

        // Return only UserId to Flutter
        return Result.Success(new RegisterResponse(user.Id));
    }

}
