namespace Wasla.Features.Marketplace.Tasks.GetTasks;

public class GetTasksHandler(IRepository<SeekerTask> taskRepo)
    : IRequestHandler<GetTasksRequest, Result<GetTasksResponse>>
{
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;

    public async Task<Result<GetTasksResponse>> Handle(GetTasksRequest request, CancellationToken ct)
    {
        var query = _taskRepo.GetAll().Where(t => !t.IsDeleted);

        if (request.Category.HasValue)
            query = query.Where(t => t.Category == request.Category.Value);

        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

        query = query.OrderByDescending(t => t.CreatedOn);

        var paginatedList = await PaginatedList<TaskListResponse>.CreateAsync(
            query.Select(t => new TaskListResponse(
                t.Id, t.Title, t.Description, t.Category,
                t.Status, t.Budget, t.Deadline, t.CreatedOn
            )),
            request.PageNumber,
            request.PageSize,
            ct
        );

        var response = new GetTasksResponse(
            paginatedList.Items,
            new PaginationResponse
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = request.PageSize,
                TotalPages = paginatedList.TotalPages,
                HasNextPage = paginatedList.HasNextPage
            }
        );

        return Result.Success(response);
    }
}
