using Wasla.Common.Enums;

namespace Wasla.Features.HelperProfileManagement.HelperServices.GetServiceById;

public record GetServiceByIdResponse(
    int Id,
    int HelperId,
    string Title,
    string Description,
    decimal Price,
    decimal? DiscountPrice,
    int DeliveryDays,
    int RevisionsCount,
    ServiceCategory Category,
    bool IsActive,
    DateTime CreatedOn,
    List<ServicePackageDto> Packages
);

public record ServicePackageDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int DeliveryDays,
    int RevisionsCount,
    PackageType Type
);
