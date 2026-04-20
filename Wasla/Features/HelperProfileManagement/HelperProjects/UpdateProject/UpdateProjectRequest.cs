namespace Wasla.Features.HelperProfileManagement.HelperProjects.UpdateProject;

public record UpdateProjectRequest(
    int Id,
    string Title,
    string Description,
    string? ProjectImageUrl,
    string? RepositoryUrl,
    string? LiveDemoUrl,
    List<int>? SkillIds
) : IRequest<Result>;

public class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
    }
}
