namespace Wasla.Features.HelperProfileManagement.HelperProjects.CreateProject;

public record CreateHelperProjectRequest(
    string Title,
    string Description,
    string? ProjectImageUrl,
    string? RepositoryUrl,
    string? LiveDemoUrl,
    List<int>? SkillIds
) : IRequest<Result<CreateProjectResponse>>;

public class CreateProjectRequestValidator : AbstractValidator<CreateHelperProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.ProjectImageUrl).MaximumLength(500).When(x => x.ProjectImageUrl is not null);
        RuleFor(x => x.RepositoryUrl).MaximumLength(500).When(x => x.RepositoryUrl is not null);
        RuleFor(x => x.LiveDemoUrl).MaximumLength(500).When(x => x.LiveDemoUrl is not null);
    }
}
