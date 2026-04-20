namespace Wasla.Features.ProfileManagement.Seeker.UpdateSeekerProfile;

public record UpdateSeekerProfileRequest(
    string? Location,
    string? CompanyName
) : IRequest<Result>;

public class UpdateSeekerProfileRequestValidator : AbstractValidator<UpdateSeekerProfileRequest>
{
    public UpdateSeekerProfileRequestValidator()
    {
        RuleFor(x => x.Location).MaximumLength(200).When(x => x.Location != null);
        RuleFor(x => x.CompanyName).MaximumLength(200).When(x => x.CompanyName != null);
    }
}
