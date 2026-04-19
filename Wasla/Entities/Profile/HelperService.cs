using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Profile;


public class HelperService : AuditableEntity
{
    public int Id { get; set; }
    public int HelperId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; } 
    public int DeliveryDays { get; set; }
    public int RevisionsCount { get; set; } // Number of revisions included in the service
    public ServiceCategory Category { get; set; }
    public bool IsActive { get; set; } = true;

    public Helper Helper { get; set; } = default!;
    public ICollection<ServicePackage> Packages { get; set; } = [];
}
