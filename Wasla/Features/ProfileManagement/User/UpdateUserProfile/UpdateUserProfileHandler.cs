using Microsoft.AspNetCore.Identity;

namespace Wasla.Features.ProfileManagement.User.UpdateUserProfile;

public class UpdateUserProfileHandler(
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateUserProfileRequest, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(UpdateUserProfileRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == null)
            return Result.Failure(ProfileErrors.UserNotFound);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result.Failure(ProfileErrors.UserNotFound);

        bool updated = false;

        if (request.Name != null && user.Name != request.Name)
        {
            user.Name = request.Name;
            updated = true;
        }

        if (request.Bio != null && user.Bio != request.Bio)
        {
            user.Bio = request.Bio;
            updated = true;
        }

        if (request.ProfilePictureUrl != null && user.ProfilePictureUrl != request.ProfilePictureUrl)
        {
            user.ProfilePictureUrl = request.ProfilePictureUrl;
            updated = true;
        }

        if (request.PhoneNumber != null && user.PhoneNumber != request.PhoneNumber)
        {
            user.PhoneNumber = request.PhoneNumber;
            updated = true;
        }

        if (request.Country != null && user.Country != request.Country)
        {
            user.Country = request.Country;
            updated = true;
        }

        if (request.City != null && user.City != request.City)
        {
            user.City = request.City;
            updated = true;
        }

        if (request.WebsiteUrl != null && user.WebsiteUrl != request.WebsiteUrl)
        {
            user.WebsiteUrl = request.WebsiteUrl;
            updated = true;
        }

        if (request.LinkedInUrl != null && user.LinkedInUrl != request.LinkedInUrl)
        {
            user.LinkedInUrl = request.LinkedInUrl;
            updated = true;
        }

        if (request.GitHubUrl != null && user.GitHubUrl != request.GitHubUrl)
        {
            user.GitHubUrl = request.GitHubUrl;
            updated = true;
        }

        if (updated)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result.Failure(new Error("Profile.UpdateFailed", errors, StatusCodes.Status400BadRequest));
            }
        }

        return Result.Success();
    }
}
