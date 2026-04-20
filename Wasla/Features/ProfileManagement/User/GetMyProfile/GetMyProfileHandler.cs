using Microsoft.AspNetCore.Identity;

namespace Wasla.Features.ProfileManagement.User.GetMyProfile;

public class GetMyProfileHandler(
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IRepository<Wasla.Entities.Identity.Seeker> seekerRepo,
    IRepository<Wasla.Entities.Identity.Helper> helperRepo) : IRequestHandler<GetMyProfileRequest, Result<GetMyProfileResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IRepository<Wasla.Entities.Identity.Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<Wasla.Entities.Identity.Helper> _helperRepo = helperRepo;

    public async Task<Result<GetMyProfileResponse>> Handle(GetMyProfileRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == null)
            return Result.Failure<GetMyProfileResponse>(ProfileErrors.UserNotFound);

        // Fetching user directly with EF to avoid manager overhead if we don't need identity specific stuff,
        // but UserManager is safer for Identity fields. Let's use UserManager.
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result.Failure<GetMyProfileResponse>(ProfileErrors.UserNotFound);

        var isSeeker = await _seekerRepo.AnyAsync(s => s.UserId == userId, ct);
        var isHelper = await _helperRepo.AnyAsync(h => h.UserId == userId, ct);

        // Fetch user roles
        var roles = await _userManager.GetRolesAsync(user);

        return Result.Success(new GetMyProfileResponse(
            user.Id,
            user.Name,
            user.Email ?? string.Empty,
            user.PhoneNumber,
            user.ProfilePictureUrl,
            user.Bio,
            user.Country,
            user.City,
            user.WebsiteUrl,
            user.LinkedInUrl,
            user.GitHubUrl,
            isSeeker,
            isHelper,
            // IdentityUser doesn't have CreatedOn by default.
            DateTime.UtcNow // Replace with actual if it's added later
        ));
    }
}
