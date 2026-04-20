namespace Wasla.Features.HelperProfileManagement.HelperProjects.GetHelperProjects;

public record GetHelperProjectsRequest(int HelperId) : IRequest<Result<List<HelperProjectResponse>>>;

public record HelperProjectResponse(
    int Id,
    string Title,
    string Description,
    string? ProjectImageUrl,
    string? RepositoryUrl,
    string? LiveDemoUrl,
    DateTime CreatedOn
);
