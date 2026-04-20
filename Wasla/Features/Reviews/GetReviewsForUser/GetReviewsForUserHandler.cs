namespace Wasla.Features.Reviews.GetReviewsForUser;

public class GetReviewsForUserHandler(IRepository<Review> reviewRepo)
    : IRequestHandler<GetReviewsForUserRequest, Result<GetReviewsForUserResponse>>
{
    private readonly IRepository<Review> _reviewRepo = reviewRepo;

    public async Task<Result<GetReviewsForUserResponse>> Handle(GetReviewsForUserRequest request, CancellationToken ct)
    {
        var query = _reviewRepo
            .FindAll(r => r.RevieweeId == request.UserId && r.IsVisible)
            .OrderByDescending(r => r.CreatedOn);

        var totalCount = await query.CountAsync(ct);
        var averageRating = totalCount > 0 ? await query.AverageAsync(r => r.Rating, ct) : 0;

        var paginatedList = await PaginatedList<ReviewResponse>.CreateAsync(
            query.Select(r => new ReviewResponse(
                r.Id, r.ReviewerId, r.Type, r.Rating, r.Comment,
                r.QualityRating, r.CommunicationRating, r.TimelinessRating, r.ValueRating, r.CreatedOn
            )),
            request.PageNumber, request.PageSize, ct
        );

        return Result.Success(new GetReviewsForUserResponse(
            paginatedList.Items,
            new PaginationResponse
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = request.PageSize,
                TotalPages = paginatedList.TotalPages,
                HasNextPage = paginatedList.HasNextPage
            },
            averageRating,
            totalCount
        ));
    }
}
