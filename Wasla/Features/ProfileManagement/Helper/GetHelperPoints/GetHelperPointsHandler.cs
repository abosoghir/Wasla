namespace Wasla.Features.ProfileManagement.Helper.GetHelperPoints;

public class GetHelperPointsHandler(
    IRepository<Wasla.Entities.Identity.Helper> helperRepo,
    IRepository<PointTransaction> pointTransactionRepo,
    IRepository<UserBadge> userBadgeRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetHelperPointsRequest, Result<GetHelperPointsResponse>>
{
    private readonly IRepository<Wasla.Entities.Identity.Helper> _helperRepo = helperRepo;
    private readonly IRepository<PointTransaction> _pointTransactionRepo = pointTransactionRepo;
    private readonly IRepository<UserBadge> _userBadgeRepo = userBadgeRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetHelperPointsResponse>> Handle(GetHelperPointsRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure<GetHelperPointsResponse>(ProfileErrors.UserNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper == null)
            return Result.Failure<GetHelperPointsResponse>(ProfileErrors.HelperProfileNotFound);

        // Get Badges
        var badges = await _userBadgeRepo.GetAll()
            .Include(ub => ub.Badge)
            .Where(ub => ub.UserId == userId && !ub.IsDeleted && ub.IsDisplayed)
            .OrderBy(ub => ub.DisplayOrder)
            .Select(ub => new BadgeDto(
                ub.Badge.Id,
                ub.Badge.Name,
                ub.Badge.Description,
                ub.Badge.IconUrl,
                ub.EarnedAt,
                ub.IsDisplayed
            ))
            .ToListAsync(ct);

        // Get Recent Point History
        var history = await _pointTransactionRepo.GetAll()
            .Where(pt => pt.HelperId == helper.Id && !pt.IsDeleted)
            .OrderByDescending(pt => pt.CreatedOn)
            .Take(10)
            .Select(pt => new PointHistoryDto(
                pt.Id,
                pt.Type,
                pt.Points,
                pt.BalanceAfter,
                pt.Description,
                pt.CreatedOn
            ))
            .ToListAsync(ct);

        // Get Leaderboard (Top 8 helpers)
        var topHelpers = await _helperRepo.GetAll()
            .Include(h => h.User)
            .Where(h => !h.IsDeleted)
            .OrderByDescending(h => h.LifetimePoints)
            .ThenByDescending(h => h.CompletedTasksCount)
            .Take(8)
            .ToListAsync(ct);

        var leaderboard = topHelpers.Select((h, index) => new LeaderboardEntryDto(
            index + 1,
            h.Id,
            h.User.Name,
            h.User.ProfilePictureUrl,
            h.LifetimePoints,
            h.CompletedTasksCount,
            h.AverageRating
        )).ToList();

        return Result.Success(new GetHelperPointsResponse(
            helper.Points,
            helper.LifetimePoints,
            badges,
            history,
            leaderboard
        ));
    }
}
