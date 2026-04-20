using Wasla.Features.Marketplace;
using Wasla.Features.Marketplace.ProjectMilestones.CreateMilestone;

namespace Wasla.Features.Marketplace.ProjectMilestones.CreateMilestone;

public class CreateMilestoneHandler(
    IRepository<Project> projectRepo,
    IRepository<ProjectMilestone> milestoneRepo,
    IRepository<Seeker> seekerRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateMilestoneRequest, Result<CreateMilestoneResponse>>
{
    private readonly IRepository<Project> _projectRepo = projectRepo;
    private readonly IRepository<ProjectMilestone> _milestoneRepo = milestoneRepo;
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateMilestoneResponse>> Handle(CreateMilestoneRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var project = await _projectRepo.GetByIdAsync(request.ProjectId, ct);
        if (project is null)
            return Result.Failure<CreateMilestoneResponse>(MarketplaceErrors.ProjectNotFound);

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null || project.SeekerId != seeker.Id)
            return Result.Failure<CreateMilestoneResponse>(MarketplaceErrors.Unauthorized);

        var milestone = new ProjectMilestone
        {
            ProjectId = request.ProjectId,
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
            OrderIndex = request.OrderIndex,
            DueDate = request.DueDate,
            Status = MilestoneStatus.Pending,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _milestoneRepo.AddAsync(milestone, ct);
        await _milestoneRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateMilestoneResponse(milestone.Id));
    }
}
