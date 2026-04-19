namespace Wasla.Features.Marketplace.Tasks.GetTasks;

public record GetTasksRequest(TaskCategory? Category, Common.Enums.TaskStatus? Status, int PageNumber = 1, int PageSize = 10) : IRequest<Result<GetTasksResponse>>;

public record GetTasksResponse(List<TaskListResponse> Items, PaginationResponse Pagination);

public record TaskListResponse(int Id, string Title, string Description, TaskCategory Category, Common.Enums.TaskStatus Status, decimal? Budget, DateTime? Deadline, DateTime CreatedOn);

public class GetTasksRequestValidator : AbstractValidator<GetTasksRequest>
{
    public GetTasksRequestValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
    }
}
