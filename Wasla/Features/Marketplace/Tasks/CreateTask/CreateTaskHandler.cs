namespace Wasla.Features.Marketplace.Tasks.CreateTask;

public class CreateTaskHandler(
    IRepository<Seeker> seekerRepo,
    IRepository<SeekerTask> taskRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateTaskRequest, Result<CreateTaskResponse>>
{
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateTaskResponse>> Handle(CreateTaskRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null)
            return Result.Failure<CreateTaskResponse>(MarketplaceErrors.SeekerNotFound);

        var task = new SeekerTask
        {
            SeekerId = seeker.Id,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Status = Common.Enums.TaskStatus.Open,
            Budget = request.Budget,
            Deadline = request.Deadline,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _taskRepo.AddAsync(task, ct);

        seeker.TotalTasksPosted++;
        await _seekerRepo.UpdateAsync(seeker, ct);

        await _taskRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateTaskResponse(task.Id));
    }
}
