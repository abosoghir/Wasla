namespace Wasla.Features.HelperProfileManagement.HelperProjects.CreateProject;

public class CreateProjectHandler(
    IRepository<Helper> helperRepo,
    IRepository<HelperProject> projectRepo,
    IRepository<ProjectSkill> projectSkillRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateHelperProjectRequest, Result<CreateProjectResponse>>
{
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<HelperProject> _projectRepo = projectRepo;
    private readonly IRepository<ProjectSkill> _projectSkillRepo = projectSkillRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateProjectResponse>> Handle(CreateHelperProjectRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null)
            return Result.Failure<CreateProjectResponse>(HelperProfileErrors.HelperNotFound);

        var project = new HelperProject
        {
            HelperId = helper.Id,
            Title = request.Title,
            Description = request.Description,
            ProjectImageUrl = request.ProjectImageUrl,
            RepositoryUrl = request.RepositoryUrl,
            LiveDemoUrl = request.LiveDemoUrl,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _projectRepo.AddAsync(project, ct);
        await _projectRepo.SaveChangesAsync(ct);

        // Attach skills
        if (request.SkillIds is { Count: > 0 })
        {
            var skills = request.SkillIds.Select(skillId => new ProjectSkill
            {
                ProjectId = project.Id,
                SkillId = skillId,
                CreatedById = userId!,
                CreatedOn = DateTime.UtcNow
            });

            await _projectSkillRepo.AddRangeAsync(skills, ct);
            await _projectSkillRepo.SaveChangesAsync(ct);
        }

        return Result.Success(new CreateProjectResponse(project.Id));
    }
}
