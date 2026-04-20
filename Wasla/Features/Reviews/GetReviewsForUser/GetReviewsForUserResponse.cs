namespace Wasla.Features.Reviews.GetReviewsForUser;

public record GetReviewsForUserResponse(List<ReviewResponse> Items, PaginationResponse Pagination, double AverageRating, int TotalCount);

public record ReviewResponse(int Id, string ReviewerId, ReviewType Type, int Rating, string? Comment,
    int? QualityRating, int? CommunicationRating, int? TimelinessRating, int? ValueRating, DateTime CreatedOn);

