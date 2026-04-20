namespace Wasla.Features.HelperProfileManagement.HelperProjects.GetProjectById;

public class GetProjectByIdHandler(IRepository<HelperProject> projectRepo)
    : IRequestHandler<GetProjectByIdRequest, Result<GetProjectByIdResponse>>
{
    private readonly IRepository<HelperProject> _projectRepo = projectRepo;

    public async Task<Result<GetProjectByIdResponse>> Handle(GetProjectByIdRequest request, CancellationToken ct)
    {
        var project = await _projectRepo
            .Include(p => p.ProjectSkills)
            .Where(p => p.Id == request.Id && !p.IsDeleted)
            .Select(p => new GetProjectByIdResponse(
                p.Id, p.HelperId, p.Title, p.Description,
                p.ProjectImageUrl, p.RepositoryUrl, p.LiveDemoUrl,
                p.CreatedOn,
                p.ProjectSkills.Select(ps => new ProjectSkillDto(ps.SkillId, ps.Skill.Name)).ToList()
            ))
            .FirstOrDefaultAsync(ct);

        if (project is null)
            return Result.Failure<GetProjectByIdResponse>(HelperProfileErrors.ProjectNotFound);

        return Result.Success(project);
    }
}
