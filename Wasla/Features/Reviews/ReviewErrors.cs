namespace Wasla.Features.Reviews;

public static class ReviewErrors
{
    public static readonly Error ReviewNotFound =
        new("Review.ReviewNotFound", "Review not found.", StatusCodes.Status404NotFound);

    public static readonly Error DuplicateReview =
        new("Review.DuplicateReview", "You have already reviewed this entity.", StatusCodes.Status409Conflict);

    public static readonly Error InvalidRating =
        new("Review.InvalidRating", "Rating must be between 1 and 5.", StatusCodes.Status400BadRequest);

    public static readonly Error CannotReviewSelf =
        new("Review.CannotReviewSelf", "You cannot review yourself.", StatusCodes.Status400BadRequest);
}
