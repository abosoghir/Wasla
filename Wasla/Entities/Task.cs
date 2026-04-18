using Wasla.Entities.Users;

namespace Wasla.Entities;

public class Task
{
    public int Id { get; set; }
    public int SeekerId { get; set; }

    public int? HelperId { get; set; } 

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty; // e.g. "WebBackend", "WebFrontend", "Math"

    public TaskStatus Status { get; set; } = TaskStatus.Open;

    public decimal? Budget { get; set; }

    public int PointsAwarded { get; set; } = 10;

    public bool IsFreeTask { get; set; } = false;

    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public Seeker Seeker { get; set; } = default!;
    public Helper? Helper { get; set; }
    public ICollection<TaskOffer> Offers { get; set; } = [];
    public Review? Review { get; set; }
    public Payment? Payment { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}

public enum TaskStatus
{
    Open = 0,
    InProgress = 1,
    UnderReview = 2,
    Completed = 3,
    Cancelled = 4
}