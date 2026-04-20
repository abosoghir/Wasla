namespace Wasla.Features.HelperProfileManagement.HelperProjects.UpdateProject;

public class UpdateProjectHandler(
    IRepository<HelperProject> projectRepo,
    IRepository<Helper> helperRepo,
    IRepository<ProjectSkill> projectSkillRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<UpdateProjectRequest, Result>
{
    private readonly IRepository<HelperProject> _projectRepo = projectRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<ProjectSkill> _projectSkillRepo = projectSkillRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(UpdateProjectRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var project = await _projectRepo.GetByIdAsync(request.Id, ct);
        if (project is null)
            return Result.Failure(HelperProfileErrors.ProjectNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null || project.HelperId != helper.Id)
            return Result.Failure(HelperProfileErrors.Unauthorized);

        project.Title = request.Title;
        project.Description = request.Description;
        project.ProjectImageUrl = request.ProjectImageUrl;
        project.RepositoryUrl = request.RepositoryUrl;
        project.LiveDemoUrl = request.LiveDemoUrl;
        project.UpdatedById = userId;
        project.UpdatedOn = DateTime.UtcNow;

        await _projectRepo.UpdateAsync(project, ct);

        // Replace all skills (simplest for MVP)
        if (request.SkillIds is not null)
        {
            await _projectSkillRepo.BulkDeleteWhereAsync(
                ps => ps.ProjectId == project.Id, ct);

            if (request.SkillIds.Count > 0)
            {
                var skills = request.SkillIds.Select(skillId => new ProjectSkill
                {
                    ProjectId = project.Id,
                    SkillId = skillId,
                    CreatedById = userId!,
                    CreatedOn = DateTime.UtcNow
                });

                await _projectSkillRepo.AddRangeAsync(skills, ct);
            }
        }

        await _projectRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
