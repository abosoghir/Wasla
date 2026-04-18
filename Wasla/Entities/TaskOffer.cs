using EduBrain.Entities.Common;
using Wasla.Entities.Users;

namespace Wasla.Entities;

public class TaskOffer : AuditableEntity
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int HelperId { get; set; }

    public string? Message { get; set; }

    public decimal ProposedPrice { get; set; }

    public TaskOfferStatus Status { get; set; } = TaskOfferStatus.Pending;

    // Navigation properties
    public Task Task { get; set; } = default!;
    public Helper Helper { get; set; } = default!;
}

public enum TaskOfferStatus
{
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
    Withdrawn = 3
}
