namespace Wasla.Features.Marketplace.Projects.GetProjects;

public record GetProjectsRequest(ProjectCategory? Category, ProjectStatus? Status, int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<GetProjectsResponse>>;

public record GetProjectsResponse(List<ProjectListResponse> Items, PaginationResponse Pagination);

public record ProjectListResponse(int Id, string Title, string Description, decimal Budget, int DurationDays,
    ProjectCategory Category, ProjectStatus Status, DateTime CreatedOn);

public class GetProjectsRequestValidator : AbstractValidator<GetProjectsRequest>
{
    public GetProjectsRequestValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
    }
}
