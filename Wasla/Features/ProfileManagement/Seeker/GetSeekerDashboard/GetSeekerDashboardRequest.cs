namespace Wasla.Features.ProfileManagement.Seeker.GetSeekerDashboard;

public record GetSeekerDashboardRequest() : IRequest<Result<GetSeekerDashboardResponse>>;

public record GetSeekerDashboardResponse(
    int TotalTasksPosted,
    int TotalSessionsBooked,
    decimal TotalAmountSpent,
    int ActiveTasksCount,
    int ActiveProjectsCount
);
