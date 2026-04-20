namespace Wasla.Features.HelperProfileManagement.HelperProjects.GetHelperProjects;

public class GetHelperProjectsHandler(IRepository<HelperProject> projectRepo)
    : IRequestHandler<GetHelperProjectsRequest, Result<List<HelperProjectResponse>>>
{
    private readonly IRepository<HelperProject> _projectRepo = projectRepo;

    public async Task<Result<List<HelperProjectResponse>>> Handle(GetHelperProjectsRequest request, CancellationToken ct)
    {
        var projects = await _projectRepo
            .FindAll(p => p.HelperId == request.HelperId && !p.IsDeleted)
            .Select(p => new HelperProjectResponse(
                p.Id, p.Title, p.Description,
                p.ProjectImageUrl, p.RepositoryUrl, p.LiveDemoUrl, p.CreatedOn
            ))
            .ToListAsync(ct);

        return Result.Success(projects);
    }
}
