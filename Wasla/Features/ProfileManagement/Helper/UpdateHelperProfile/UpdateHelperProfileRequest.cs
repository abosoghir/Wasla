namespace Wasla.Features.ProfileManagement.Helper.UpdateHelperProfile;

public record UpdateHelperProfileRequest(
    string? Headline,
    string? Location,
    decimal? HourlyRate,
    bool? IsAvailable
) : IRequest<Result>;

public class UpdateHelperProfileRequestValidator : AbstractValidator<UpdateHelperProfileRequest>
{
    public UpdateHelperProfileRequestValidator()
    {
        RuleFor(x => x.Headline).MaximumLength(200).When(x => x.Headline != null);
        RuleFor(x => x.Location).MaximumLength(200).When(x => x.Location != null);
        RuleFor(x => x.HourlyRate).GreaterThanOrEqualTo(0).When(x => x.HourlyRate.HasValue);
    }
}
