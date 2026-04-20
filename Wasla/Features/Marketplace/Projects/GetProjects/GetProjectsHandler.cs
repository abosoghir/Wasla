namespace Wasla.Features.Marketplace.Projects.GetProjects;

public class GetProjectsHandler(IRepository<Project> projectRepo)
    : IRequestHandler<GetProjectsRequest, Result<GetProjectsResponse>>
{
    private readonly IRepository<Project> _projectRepo = projectRepo;

    public async Task<Result<GetProjectsResponse>> Handle(GetProjectsRequest request, CancellationToken ct)
    {
        var query = _projectRepo.GetAll()
            .Where(p => !p.IsDeleted && p.IsPublic);

        if (request.Category.HasValue)
            query = query.Where(p => p.Category == request.Category.Value);

        if (request.Status.HasValue)
            query = query.Where(p => p.Status == request.Status.Value);

        query = query.OrderByDescending(p => p.CreatedOn);

        var paginatedList = await PaginatedList<ProjectListResponse>.CreateAsync(
            query.Select(p => new ProjectListResponse(
                p.Id, p.Title, p.Description, p.Budget, p.DurationDays,
                p.Category, p.Status, p.CreatedOn
            )),
            request.PageNumber, request.PageSize, ct
        );

        return Result.Success(new GetProjectsResponse(
            paginatedList.Items,
            new PaginationResponse
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = request.PageSize,
                TotalPages = paginatedList.TotalPages,
                HasNextPage = paginatedList.HasNextPage
            }
        ));
    }
}
