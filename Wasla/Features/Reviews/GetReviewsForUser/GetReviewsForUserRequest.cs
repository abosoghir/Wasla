namespace Wasla.Features.Reviews.GetReviewsForUser;

public record GetReviewsForUserRequest(string UserId, int PageNumber = 1, int PageSize = 20)
    : IRequest<Result<GetReviewsForUserResponse>>;

