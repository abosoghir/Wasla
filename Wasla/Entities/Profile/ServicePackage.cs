using Wasla.Common.Enums;
using Wasla.Entities.Common;

namespace Wasla.Entities.Profile;

public class ServicePackage : AuditableEntity
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DeliveryDays { get; set; }
    public int RevisionsCount { get; set; }
    public PackageType Type { get; set; }

    public HelperService Service { get; set; } = default!;
}
