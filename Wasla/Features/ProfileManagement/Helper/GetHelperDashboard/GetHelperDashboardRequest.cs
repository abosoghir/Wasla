namespace Wasla.Features.ProfileManagement.Helper.GetHelperDashboard;

public record GetHelperDashboardRequest() : IRequest<Result<GetHelperDashboardResponse>>;

public record GetHelperDashboardResponse(
    int Points,
    int LifetimePoints,
    int CompletedTasksCount,
    int CompletedSessionsCount,
    int CompletedProjectsCount,
    double AverageRating,
    int TotalReviewsCount,
    decimal TotalEarnings,
    bool IsNextTaskFree,
    double SpeedOfResponseInMintues // keeping the typo to match the entity
);
