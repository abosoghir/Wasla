namespace Wasla.Features.ProfileManagement.User.GetMyProfile;

public record GetMyProfileRequest() : IRequest<Result<GetMyProfileResponse>>;

public record GetMyProfileResponse(
    string Id,
    string Name,
    string Email,
    string? PhoneNumber,
    string? ProfilePictureUrl,
    string? Bio,
    string? Country,
    string? City,
    string? WebsiteUrl,
    string? LinkedInUrl,
    string? GitHubUrl,
    bool IsSeeker,
    bool IsHelper,
    DateTime CreatedOn
);
