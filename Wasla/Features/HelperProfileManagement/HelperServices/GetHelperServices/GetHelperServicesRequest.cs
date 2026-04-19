namespace Wasla.Features.HelperProfileManagement.HelperServices.GetHelperServices;

public record GetHelperServicesRequest(int HelperId) : IRequest<Result<List<HelperServiceResponse>>>;

public record HelperServiceResponse(
    int Id,
    string Title,
    string Description,
    decimal Price,
    decimal? DiscountPrice,
    int DeliveryDays,
    ServiceCategory Category,
    DateTime CreatedOn
);
