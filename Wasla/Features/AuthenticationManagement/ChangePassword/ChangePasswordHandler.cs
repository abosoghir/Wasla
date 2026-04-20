using Microsoft.AspNetCore.Identity;

namespace Wasla.Features.AuthenticationManagement.ChangePassword;

public class ChangePasswordHandler(
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<ChangePasswordRequest, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(ChangePasswordRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == null)
            return Result.Failure(UserErrors.UserNotFound);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result.Failure(UserErrors.UserNotFound);

        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(new Error("ChangePassword.Failed", errors, StatusCodes.Status400BadRequest));
        }

        return Result.Success();
    }
}
