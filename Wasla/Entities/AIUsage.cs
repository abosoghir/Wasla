using EduBrain.Entities.Users;

namespace Wasla.Entities;

/// <summary>
/// Tracks AI-powered feature usage — paid with points (400+ pts threshold)
/// or via direct subscription. Feature types: ProfileEnhancement, CodeReview, etc.
/// </summary>
public class AIUsage : AuditableEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FeatureType { get; set; } = string.Empty;

    public int PointsSpent { get; set; } = 0;

    public string? InputData { get; set; }

    public string? OutputData { get; set; }

    public DateTime UsedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ApplicationUser User { get; set; } = null!;
}