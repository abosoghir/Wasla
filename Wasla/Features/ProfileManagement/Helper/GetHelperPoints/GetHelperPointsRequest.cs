namespace Wasla.Features.ProfileManagement.Helper.GetHelperPoints;

public record GetHelperPointsRequest() : IRequest<Result<GetHelperPointsResponse>>;

public record GetHelperPointsResponse(
    int CurrentPoints,
    int LifetimePoints,
    List<BadgeDto> Badges,
    List<PointHistoryDto> RecentPointHistory,
    List<LeaderboardEntryDto> Leaderboard
);

public record BadgeDto(
    int Id,
    string Name,
    string Description,
    string IconUrl,
    DateTime EarnedAt,
    bool IsDisplayed
);

public record PointHistoryDto(
    int Id,
    PointTransactionType Type,
    int Points,
    int BalanceAfter,
    string? Description,
    DateTime CreatedOn
);

public record LeaderboardEntryDto(
    int Rank,
    int HelperId,
    string Name,
    string? Avatar,
    int LifetimePoints,
    int CompletedTasksCount,
    double AverageRating
);
