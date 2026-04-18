using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class Session : AuditableEntity
{
    public int Id { get; set; } 

    public int SeekerId { get; set; }

    public int HelperId { get; set; }

    public DateTime ScheduledAt { get; set; }

    public int DurationMinutes { get; set; }

    public decimal TotalPrice { get; set; }



    public SessionStatus Status { get; set; } = SessionStatus.Pending;

    /// <summary>
    /// True when this session is gifted (helper's every 5th session — no payment taken).
    /// </summary>
    public bool IsFreeSession { get; set; } = false;

    // Navigation properties
    public Seeker Seeker { get; set; } = default!;
    public Helper Helper { get; set; } = default!;
    public Review? Review { get; set; }
    public Payment? Payment { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}

public enum SessionStatus
{
    Pending = 0,
    Confirmed = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}