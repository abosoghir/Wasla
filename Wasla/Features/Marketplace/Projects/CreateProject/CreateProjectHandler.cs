namespace Wasla.Features.Marketplace.Projects.CreateProject;

public class CreateProjectHandler(
    IRepository<Seeker> seekerRepo,
    IRepository<Project> projectRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateProjectRequest, Result<CreateProjectResponse>>
{
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<Project> _projectRepo = projectRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateProjectResponse>> Handle(CreateProjectRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null)
            return Result.Failure<CreateProjectResponse>(MarketplaceErrors.SeekerNotFound);

        var project = new Project
        {
            SeekerId = seeker.Id,
            Title = request.Title,
            Description = request.Description,
            Budget = request.Budget,
            DurationDays = request.DurationDays,
            Category = request.Category,
            RequiredSkills = request.RequiredSkills,
            IsPublic = request.IsPublic,
            Status = ProjectStatus.Open,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _projectRepo.AddAsync(project, ct);
        await _projectRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateProjectResponse(project.Id));
    }
}
