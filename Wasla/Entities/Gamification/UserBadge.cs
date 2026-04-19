using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Gamification;

public class UserBadge : AuditableEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int BadgeId { get; set; }
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
    public bool IsDisplayed { get; set; } = true; // Whether to show this badge on the user's profile
    public int DisplayOrder { get; set; } // For ordering badges on the user's profile

    public ApplicationUser User { get; set; } = default!;
    public Badge Badge { get; set; } = default!;
}
