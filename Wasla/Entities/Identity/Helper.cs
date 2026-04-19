namespace Wasla.Entities.Identity;

public class Helper : AuditableEntity
{
    public int Id { get; set; } 
    public string UserId { get; set; } = string.Empty;

    public string? Headline { get; set; } // A short, catchy description of the helper's expertise
    public string? Location { get; set; }
    public decimal HourlyRate { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsVerified { get; set; } = false;

    // Gamification - MVP Simplified
    public int Points { get; set; }
    public int LifetimePoints { get; set; }
    public int CompletedTasksCount { get; set; }
    public int CompletedSessionsCount { get; set; }
    public int CompletedProjectsCount { get; set; }
    public double SpeedOfResponseInMintues { get; set; } // Average time (in hours) to respond to offers or messages

    // Every 5th task is free (gifted) — checked in service layer
    public bool IsNextTaskFree => CompletedTasksCount > 0 && CompletedTasksCount % 4 == 0;

    // Ratings - Aggregated
    public double AverageRating { get; set; }
    public int TotalReviewsCount { get; set; }

    // Financial
    public decimal TotalEarnings { get; set; }

    public ApplicationUser User { get; set; } = default!;
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<TaskOffer> TaskOffers { get; set; } = []; // Offers made by the helper on tasks
    public ICollection<ProjectOffer> ProjectOffers { get; set; } = []; // Offers made by the helper on projects
    public ICollection<Review> ReviewsReceived { get; set; } = []; // Reviews about the helper from seekers
    public ICollection<PointTransaction> PointTransactions { get; set; } = []; // History of points earned/spent by the helper

    // Profile
    public ICollection<HelperSkill> HelperSkills { get; set; } = []; // Many-to-many relationship with skills through HelperSkill
    public ICollection<HelperProject> Projects { get; set; } = [];
    public ICollection<HelperService> Services { get; set; } = [];
}
