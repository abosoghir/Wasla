using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.AI;

public class AIUsage : AuditableEntity  
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public AIFeatureType FeatureType { get; set; }
    public int PointsCost { get; set; } // Store the points cost at the time of usage for historical accuracy
    public int? InputLength { get; set; } // Optional: Store the length of the input (e.g., number of tokens or characters)
    public int? OutputLength { get; set; } // Optional: Store the length of the output (e.g., number of tokens or characters)
    public AIRequestStatus Status { get; set; } = AIRequestStatus.Success;
    public string? ErrorMessage { get; set; }
    public int? PointTransactionId { get; set; } // Optional: Store the related PointTransactionId if points were deducted for this usage
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;

    public ApplicationUser User { get; set; } = default!;
    public PointTransaction? PointTransaction { get; set; }
}
