namespace Wasla.Features.HelperBrowse.GetHelperById;

public record GetHelperByIdResponse(
    int Id,
    string UserId,
    string Name,
    string? ProfilePictureUrl,
    string? Bio,
    string? Headline,
    string? Location,
    decimal HourlyRate,
    bool IsAvailable,
    bool IsVerified,
    double AverageRating,
    int TotalReviewsCount,
    int CompletedTasksCount,
    int CompletedProjectsCount,
    int CompletedSessionsCount,
    decimal TotalEarnings,
    int Points,
    double SpeedOfResponseInMinutes,
    DateTime CreatedOn,
    List<string> Skills,
    List<HelperServiceResponse> Services,
    List<HelperPortfolioResponse> Portfolio,
    List<HelperReviewResponse> Reviews
);

public record HelperServiceResponse(int Id, string Title, string Description, ServiceCategory Category, decimal Price, int DeliveryDays);

public record HelperPortfolioResponse(int Id, string Title, string? Description, string? ImageUrl);

public record HelperReviewResponse(int Id, string ReviewerName, string? ReviewerAvatar, int Rating, string? Comment, ReviewType Type, DateTime CreatedOn);

