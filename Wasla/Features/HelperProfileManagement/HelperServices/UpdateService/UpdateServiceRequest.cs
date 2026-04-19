using Wasla.Common.Enums;

namespace Wasla.Features.HelperProfileManagement.HelperServices.UpdateService;

public record UpdateServiceRequest(
    int Id,
    string Title,
    string Description,
    decimal Price,
    decimal? DiscountPrice,
    int DeliveryDays,
    int RevisionsCount,
    ServiceCategory Category
) : IRequest<Result>;

public class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequest>
{
    public UpdateServiceRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.DiscountPrice)
            .LessThan(x => x.Price)
            .When(x => x.DiscountPrice.HasValue);
        RuleFor(x => x.DeliveryDays).GreaterThan(0);
        RuleFor(x => x.RevisionsCount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Category).IsInEnum();
    }
}
