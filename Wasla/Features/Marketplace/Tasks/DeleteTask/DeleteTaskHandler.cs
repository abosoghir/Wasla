namespace Wasla.Features.Marketplace.Tasks.DeleteTask;

public class DeleteTaskHandler(
    IRepository<SeekerTask> taskRepo,
    IRepository<Seeker> seekerRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<DeleteTaskRequest, Result>
{
    private readonly IRepository<SeekerTask> _taskRepo = taskRepo;
    private readonly IRepository<Seeker> _seekerRepo = seekerRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(DeleteTaskRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var task = await _taskRepo.GetByIdAsync(request.Id, ct);
        if (task is null)
            return Result.Failure(MarketplaceErrors.TaskNotFound);

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker is null || task.SeekerId != seeker.Id)
            return Result.Failure(MarketplaceErrors.Unauthorized);

        if (task.Status != Common.Enums.TaskStatus.Open)
            return Result.Failure(MarketplaceErrors.TaskNotOpen);

        task.Status = Common.Enums.TaskStatus.Cancelled;
        task.IsDeleted = true;
        task.UpdatedById = userId;
        task.UpdatedOn = DateTime.UtcNow;

        await _taskRepo.UpdateAsync(task, ct);
        await _taskRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
