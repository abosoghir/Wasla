using Wasla.Common.Gamification;

namespace Wasla.Features.Reviews.CreateReview;

public class CreateReviewHandler(
    IRepository<Review> reviewRepo,
    IRepository<Helper> helperRepo,
    IRepository<Notification> notificationRepo,
    GamificationHelper gamificationHelper,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateReviewRequest, Result<CreateReviewResponse>>
{
    private readonly IRepository<Review> _reviewRepo = reviewRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly GamificationHelper _gamificationHelper = gamificationHelper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<CreateReviewResponse>> Handle(CreateReviewRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == request.RevieweeId)
            return Result.Failure<CreateReviewResponse>(ReviewErrors.CannotReviewSelf);

        // Check for duplicate review by the same reviewer for the same reviewee and entity
        var duplicate = await _reviewRepo.AnyAsync(
            r => r.ReviewerId == userId && r.RevieweeId == request.RevieweeId
                 && r.Type == request.Type && r.RelatedEntityId == request.RelatedEntityId, ct);

        if (duplicate)
            return Result.Failure<CreateReviewResponse>(ReviewErrors.DuplicateReview);

        var review = new Review
        {
            ReviewerId = userId!,
            RevieweeId = request.RevieweeId,
            Type = request.Type,
            RelatedEntityId = request.RelatedEntityId,
            RelatedEntityType = request.RelatedEntityType,
            Rating = request.Rating,
            Comment = request.Comment,
            QualityRating = request.QualityRating,
            CommunicationRating = request.CommunicationRating,
            TimelinessRating = request.TimelinessRating,
            ValueRating = request.ValueRating,
            IsVerified = true,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _reviewRepo.AddAsync(review, ct);

        // Update helper's average rating
        var helper = await _helperRepo.FindAsync(h => h.UserId == request.RevieweeId, ct);
        if (helper is not null)
        {
            var totalReviews = helper.TotalReviewsCount + 1;
            helper.AverageRating = ((helper.AverageRating * helper.TotalReviewsCount) + request.Rating) / totalReviews;
            helper.TotalReviewsCount = totalReviews;
            await _helperRepo.UpdateAsync(helper, ct);

            // Award +5 points for positive review (rating >= 4)
            if (request.Rating >= 4)
            {
                await _gamificationHelper.AddPointsAsync(
                    helper, 5, PointTransactionType.Earned,
                    $"Positive review received ({request.Rating}★)", review.Id, "Review", userId!, ct);
            }

            // Check for 5-star review badge 
            await _gamificationHelper.CheckReviewBadgeAsync(helper, request.Rating, userId!, ct);

            // Notify reviewee
            var notification = new Notification
            {
                UserId = request.RevieweeId,
                Type = NotificationType.ReviewReceived,
                Title = "New Review Received",
                Body = $"You received a {request.Rating}★ review!",
                RelatedEntityType = "Review",
                RelatedEntityId = review.Id,
                CreatedById = userId!,
                CreatedOn = DateTime.UtcNow
            };
            await _notificationRepo.AddAsync(notification, ct);
        }

        await _reviewRepo.SaveChangesAsync(ct);

        return Result.Success(new CreateReviewResponse(review.Id));
    }
}
