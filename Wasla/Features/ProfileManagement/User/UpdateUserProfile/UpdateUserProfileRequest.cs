namespace Wasla.Features.ProfileManagement.User.UpdateUserProfile;

public record UpdateUserProfileRequest(
    string? Name,
    string? Bio,
    string? ProfilePictureUrl,
    string? PhoneNumber,
    string? Country,
    string? City,
    string? WebsiteUrl,
    string? LinkedInUrl,
    string? GitHubUrl
) : IRequest<Result>;

public class UpdateUserProfileRequestValidator : AbstractValidator<UpdateUserProfileRequest>
{
    public UpdateUserProfileRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).When(x => x.Name != null);
        RuleFor(x => x.Bio).MaximumLength(500).When(x => x.Bio != null);
        RuleFor(x => x.ProfilePictureUrl).MaximumLength(1000).When(x => x.ProfilePictureUrl != null);
        RuleFor(x => x.PhoneNumber).MaximumLength(20).When(x => x.PhoneNumber != null);
        RuleFor(x => x.Country).MaximumLength(100).When(x => x.Country != null);
        RuleFor(x => x.City).MaximumLength(100).When(x => x.City != null);
        RuleFor(x => x.WebsiteUrl).MaximumLength(500).When(x => x.WebsiteUrl != null);
        RuleFor(x => x.LinkedInUrl).MaximumLength(500).When(x => x.LinkedInUrl != null);
        RuleFor(x => x.GitHubUrl).MaximumLength(500).When(x => x.GitHubUrl != null);
    }
}
