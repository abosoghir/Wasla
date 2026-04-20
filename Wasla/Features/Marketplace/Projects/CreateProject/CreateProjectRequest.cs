namespace Wasla.Features.Marketplace.Projects.CreateProject;

public record CreateProjectRequest(
    string Title, string Description, decimal Budget, int DurationDays,
    ProjectCategory Category, string? RequiredSkills, bool IsPublic
) : IRequest<Result<CreateProjectResponse>>;


public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(5000);
        RuleFor(x => x.Budget).GreaterThan(0);
        RuleFor(x => x.DurationDays).GreaterThan(0);
        RuleFor(x => x.Category).IsInEnum();
    }
}
