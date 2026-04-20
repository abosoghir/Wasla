namespace Wasla.Features.Reviews.CreateReview;

public record CreateReviewRequest(
    string RevieweeId, ReviewType Type, int? RelatedEntityId, string? RelatedEntityType,
    int Rating, string? Comment, int? QualityRating, int? CommunicationRating,
    int? TimelinessRating, int? ValueRating
) : IRequest<Result<CreateReviewResponse>>;



public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.RevieweeId).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.QualityRating).InclusiveBetween(1, 5).When(x => x.QualityRating.HasValue);
        RuleFor(x => x.CommunicationRating).InclusiveBetween(1, 5).When(x => x.CommunicationRating.HasValue);
        RuleFor(x => x.TimelinessRating).InclusiveBetween(1, 5).When(x => x.TimelinessRating.HasValue);
        RuleFor(x => x.ValueRating).InclusiveBetween(1, 5).When(x => x.ValueRating.HasValue);
    }
}
