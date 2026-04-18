using EduBrain.Entities.Common;
using EduBrain.Entities.Users;

namespace Wasla.Entities.Users;

public class Seeker : AuditableEntity
{
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    // Profile
    public string? Bio { get; set; }

    // Stats
    public int TotalTasksPosted { get; set; }
    public int TotalSessionsBooked { get; set; }
    public decimal TotalAmountSpent { get; set; }

    // Navigation
    public ApplicationUser User { get; set; } = default!;
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<Review> ReviewsGiven { get; set; } = [];

}
