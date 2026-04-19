using Wasla.Common.Enums;

namespace Wasla.Features.HelperProfileManagement.HelperServices.CreateService;

public record CreateServiceRequest(
    string Title,
    string Description,
    decimal Price,
    decimal? DiscountPrice,
    int DeliveryDays,
    int RevisionsCount,
    ServiceCategory Category
) : IRequest<Result<CreateServiceResponse>>;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty().MaximumLength(2000);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.DiscountPrice)
            .LessThan(x => x.Price)
            .When(x => x.DiscountPrice.HasValue)
            .WithMessage("Discount price must be less than the regular price.");

        RuleFor(x => x.DeliveryDays)
            .GreaterThan(0);

        RuleFor(x => x.RevisionsCount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Category)
            .IsInEnum();
    }
}
