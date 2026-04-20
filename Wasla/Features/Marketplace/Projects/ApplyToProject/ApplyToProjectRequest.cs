namespace Wasla.Features.Marketplace.Projects.ApplyToProject;

public record ApplyToProjectRequest(int ProjectId, string? Message, decimal ProposedPrice, int ProposedDurationDays)
    : IRequest<Result<ApplyToProjectResponse>>;


public class ApplyToProjectRequestValidator : AbstractValidator<ApplyToProjectRequest>
{
    public ApplyToProjectRequestValidator()
    {
        RuleFor(x => x.ProjectId).GreaterThan(0);
        RuleFor(x => x.ProposedPrice).GreaterThan(0);
        RuleFor(x => x.ProposedDurationDays).GreaterThan(0);
        RuleFor(x => x.Message).MaximumLength(2000).When(x => x.Message != null);
    }
}
