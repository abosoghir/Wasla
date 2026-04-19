using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Gamification;

public class PointTransaction : AuditableEntity
{
    public int Id { get; set; }
    public string HelperId { get; set; } = string.Empty; // The helper whose points are being modified
    public PointTransactionType Type { get; set; }
    public int Points { get; set; }
    public int BalanceAfter { get; set; } // The helper's point balance after this transaction
    public string? Description { get; set; }
    public int? RelatedEntityId { get; set; } // e.g. TaskId, BadgeId, AIUsageId
    public string? RelatedEntityType { get; set; } // e.g. "Task", "Badge", "AIUsage"

    public Helper Helper { get; set; } = default!;
}
