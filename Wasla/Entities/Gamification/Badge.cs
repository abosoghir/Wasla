using Wasla.Entities.Common;

namespace Wasla.Entities.Gamification;

// Badges (ิวัวส)
public class Badge : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g. "First Task Completed", "5 Tasks Completed", "Top Helper"
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty; // URL to badge icon image
    public int DisplayOrder { get; set; } // For ordering badges in UI
    public bool IsActive { get; set; } = true;

    public ICollection<UserBadge> UserBadges { get; set; } = [];
}

// MVP Static Badge IDs (seed data)
public static class BadgeIds
{
    public const int FirstTask = 1;
    public const int FiveTasks = 2;
    public const int TenTasks = 3;
    public const int FirstSession = 4;
    public const int First5StarReview = 5;
    public const int VerifiedHelper = 6;
}
