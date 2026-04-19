using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.TrustSafety;
// áãÓÊÎÏã íÍİÙ ÍÇÌÇÊ íÚÌÈÊå
public class Favorite : AuditableEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public FavoriteType Type { get; set; }
    public int EntityId { get; set; } // ID of the related entity (e.g., HelperId, ServiceId, ProjectId, TaskId, PostId)
    public string? Notes { get; set; }

    public ApplicationUser User { get; set; } = default!;
}
