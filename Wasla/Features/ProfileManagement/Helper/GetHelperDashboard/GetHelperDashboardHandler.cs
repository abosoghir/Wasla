namespace Wasla.Features.ProfileManagement.Helper.GetHelperDashboard;

public class GetHelperDashboardHandler(
    IRepository<Wasla.Entities.Identity.Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetHelperDashboardRequest, Result<GetHelperDashboardResponse>>
{
    private readonly IRepository<Wasla.Entities.Identity.Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetHelperDashboardResponse>> Handle(GetHelperDashboardRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == null)
            return Result.Failure<GetHelperDashboardResponse>(ProfileErrors.UserNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);

        if (helper == null)
            return Result.Failure<GetHelperDashboardResponse>(ProfileErrors.HelperProfileNotFound);

        return Result.Success(new GetHelperDashboardResponse(
            helper.Points,
            helper.LifetimePoints,
            helper.CompletedTasksCount,
            helper.CompletedSessionsCount,
            helper.CompletedProjectsCount,
            helper.AverageRating,
            helper.TotalReviewsCount,
            helper.TotalEarnings,
            helper.IsNextTaskFree,
            helper.SpeedOfResponseInMintues
        ));
    }
}
