namespace Wasla.Features.HelperProfileManagement.HelperProjects.DeleteProject;

public class DeleteProjectHandler(
    IRepository<HelperProject> projectRepo,
    IRepository<Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<DeleteProjectRequest, Result>
{
    private readonly IRepository<HelperProject> _projectRepo = projectRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(DeleteProjectRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var project = await _projectRepo.GetByIdAsync(request.Id, ct);
        if (project is null)
            return Result.Failure(HelperProfileErrors.ProjectNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null || project.HelperId != helper.Id)
            return Result.Failure(HelperProfileErrors.Unauthorized);

        project.IsDeleted = true;
        project.UpdatedById = userId;
        project.UpdatedOn = DateTime.UtcNow;

        await _projectRepo.UpdateAsync(project, ct);
        await _projectRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
