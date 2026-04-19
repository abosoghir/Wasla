using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Marketplace;

public class TaskOffer : AuditableEntity
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int HelperId { get; set; }
    public string? Message { get; set; }
    public decimal ProposedPrice { get; set; } // Optional: Helper can propose a price different from the task's budget
    public int ProposedDurationDays { get; set; } // Optional: Helper can propose a duration different from the task's deadline
    public TaskOfferStatus Status { get; set; } = TaskOfferStatus.Pending;
    public DateTime? AcceptedAt { get; set; }

    public SeekerTask Task { get; set; } = default!;
    public Helper Helper { get; set; } = default!;
}
