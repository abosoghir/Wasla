namespace Wasla.Features.HelperProfileManagement.HelperProjects.GetProjectById;

public record GetProjectByIdRequest(int Id) : IRequest<Result<GetProjectByIdResponse>>;

public record GetProjectByIdResponse(
    int Id,
    int HelperId,
    string Title,
    string Description,
    string? ProjectImageUrl,
    string? RepositoryUrl,
    string? LiveDemoUrl,
    DateTime CreatedOn,
    List<ProjectSkillDto> Skills
);

public record ProjectSkillDto(int SkillId, string SkillName);
