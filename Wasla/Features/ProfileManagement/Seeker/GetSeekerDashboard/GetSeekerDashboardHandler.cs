namespace Wasla.Features.ProfileManagement.Seeker.GetSeekerDashboard;

public class GetSeekerDashboardHandler(
    IRepository<Wasla.Entities.Identity.Seeker> seekerRepo,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetSeekerDashboardRequest, Result<GetSeekerDashboardResponse>>
{
    private readonly IRepository<Wasla.Entities.Identity.Seeker> _seekerRepo = seekerRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetSeekerDashboardResponse>> Handle(GetSeekerDashboardRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == null)
            return Result.Failure<GetSeekerDashboardResponse>(ProfileErrors.UserNotFound);

        // Include Tasks and Projects to count active ones
        var seeker = await _seekerRepo
            .Include(s => s.Tasks)
            .Include(s => s.Projects)
            .FirstOrDefaultAsync(s => s.UserId == userId, ct);

        if (seeker == null)
            return Result.Failure<GetSeekerDashboardResponse>(ProfileErrors.SeekerProfileNotFound);

        var activeTasks = seeker.Tasks.Count(t => !t.IsDeleted && (t.Status == Common.Enums.TaskStatus.Open || t.Status == Common.Enums.TaskStatus.InProgress));
        var activeProjects = seeker.Projects.Count(p => !p.IsDeleted && (p.Status == Common.Enums.ProjectStatus.Open || p.Status == Common.Enums.ProjectStatus.InProgress));

        return Result.Success(new GetSeekerDashboardResponse(
            seeker.TotalTasksPosted,
            seeker.TotalSessionsBooked,
            seeker.TotalAmountSpent,
            activeTasks,
            activeProjects
        ));
    }
}
