using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Marketplace;

public class ProjectOffer : AuditableEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int HelperId { get; set; } 
    public string? Message { get; set; }
    public decimal ProposedPrice { get; set; }
    public int ProposedDurationDays { get; set; }
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public DateTime? AcceptedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public Project Project { get; set; } = default!;
    public Helper Helper { get; set; } = default!;
}
