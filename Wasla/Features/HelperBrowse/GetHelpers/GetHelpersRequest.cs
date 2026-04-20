namespace Wasla.Features.HelperBrowse.GetHelpers;

public record GetHelpersRequest(
    string? Search,
    ServiceCategory? Category,
    double? MinRating,
    decimal? MinPrice,
    decimal? MaxPrice,
    bool? AvailableOnly,
    bool? VerifiedOnly,
    string? SortBy,
    int PageNumber = 1,
    int PageSize = 12
) : IRequest<Result<GetHelpersResponse>>;


public record HelperListResponse(
    int Id,
    string UserId,
    string Name,
    string? ProfilePictureUrl,
    string? Headline,
    string? Location,
    decimal HourlyRate,
    bool IsAvailable,
    bool IsVerified,
    double AverageRating,
    int TotalReviewsCount,
    int CompletedTasksCount,
    List<string> Skills
);
