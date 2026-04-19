namespace Wasla.Common.Gamification;

public class GamificationHelper(
    IRepository<PointTransaction> pointTransactionRepo,
    IRepository<UserBadge> userBadgeRepo,
    IRepository<Helper> helperRepo)
{
    private readonly IRepository<PointTransaction> _pointTransactionRepo = pointTransactionRepo;
    private readonly IRepository<UserBadge> _userBadgeRepo = userBadgeRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;

    public async Task AddPointsAsync(
        Helper helper,
        int points,
        PointTransactionType type,
        string description,
        int? relatedEntityId,
        string? relatedEntityType,
        string userId,
        CancellationToken ct)
    {
        helper.Points += points;
        helper.LifetimePoints += points;

        var transaction = new PointTransaction
        {
            HelperId = helper.Id,
            Type = type,
            Points = points,
            BalanceAfter = helper.Points,
            Description = description,
            RelatedEntityId = relatedEntityId,
            RelatedEntityType = relatedEntityType,
            CreatedById = userId,
            CreatedOn = DateTime.UtcNow
        };

        await _pointTransactionRepo.AddAsync(transaction, ct);
    }

    public async Task DeductPointsAsync(
        Helper helper,
        int points,
        string description,
        int? relatedEntityId,
        string? relatedEntityType,
        string userId,
        CancellationToken ct)
    {
        helper.Points -= points;

        var transaction = new PointTransaction
        {
            HelperId = helper.Id,
            Type = PointTransactionType.Spent,
            Points = -points,
            BalanceAfter = helper.Points,
            Description = description,
            RelatedEntityId = relatedEntityId,
            RelatedEntityType = relatedEntityType,
            CreatedById = userId,
            CreatedOn = DateTime.UtcNow
        };

        await _pointTransactionRepo.AddAsync(transaction, ct);
    }

    public async Task CheckAndAwardBadgesAsync(Helper helper, string userId, CancellationToken ct)
    {
        // Check each badge condition
        await TryAwardBadgeAsync(BadgeIds.FirstTask, helper.CompletedTasksCount >= 1, helper, userId, ct);
        await TryAwardBadgeAsync(BadgeIds.FiveTasks, helper.CompletedTasksCount >= 5, helper, userId, ct);
        await TryAwardBadgeAsync(BadgeIds.TenTasks, helper.CompletedTasksCount >= 10, helper, userId, ct);
        await TryAwardBadgeAsync(BadgeIds.FirstSession, helper.CompletedSessionsCount >= 1, helper, userId, ct);
        await TryAwardBadgeAsync(BadgeIds.VerifiedHelper, helper.IsVerified, helper, userId, ct);
    }

    public async Task CheckReviewBadgeAsync(Helper helper, int rating, string userId, CancellationToken ct)
    {
        if (rating == 5)
        {
            await TryAwardBadgeAsync(BadgeIds.First5StarReview, true, helper, userId, ct);
        }
    }

    private async Task TryAwardBadgeAsync(int badgeId, bool condition, Helper helper, string userId, CancellationToken ct)
    {
        if (!condition) return;

        var alreadyHas = await _userBadgeRepo.AnyAsync(
            ub => ub.UserId == helper.UserId && ub.BadgeId == badgeId, ct);

        if (alreadyHas) return;

        var userBadge = new UserBadge
        {
            UserId = helper.UserId,
            BadgeId = badgeId,
            EarnedAt = DateTime.UtcNow,
            IsDisplayed = true,
            CreatedById = userId,
            CreatedOn = DateTime.UtcNow
        };

        await _userBadgeRepo.AddAsync(userBadge, ct);
    }
}
