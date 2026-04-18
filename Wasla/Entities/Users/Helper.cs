using EduBrain.Entities.Common;
using EduBrain.Entities.Users;

namespace Wasla.Entities.Users;

public class Helper : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;

    // Profile
    public string? Bio { get; set; }
    public decimal HourlyRate { get; set; }
    public bool IsAvailable { get; set; } = true;

    // Points & Rewards
    public int Points { get; set; }
    public int CompletedTasksCount { get; set; }
    public double SpeedOfResponseInMintues { get; set; } // Average time (in hours) to respond to offers or messages
    
    // Every 5th task is free (gifted) — checked in service layer
    public bool IsNextTaskFree => CompletedTasksCount > 0 && CompletedTasksCount % 4 == 0;

    // Rating
    public double AverageRating { get; set; }
    public int TotalReviewsCount { get; set; }

    public decimal TotalEarnings { get; set; }

    // Navigation
    public ApplicationUser User { get; set; } = default!;
    public ICollection<HelperServices> Services { get; set; } = [];
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<TaskOffer> TaskOffers { get; set; } = [];
    public ICollection<Review> ReviewsReceived { get; set; } = [];
    public ICollection<PointTransaction> PointTransactions { get; set; } = [];
    public ICollection<HelperProject> Projects { get; set; } = [];
}
