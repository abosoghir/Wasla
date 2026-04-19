using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Communication;

public class Session : AuditableEntity
{
    public int Id { get; set; }
    public string SeekerId { get; set; } = string.Empty;
    public string HelperId { get; set; } = string.Empty;

    public DateTime ScheduledAt { get; set; }
    public int DurationMinutes { get; set; }
    public decimal TotalPrice { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Pending;

    public string? MeetingLink { get; set; }
    public string? MeetingPlatform { get; set; }
    public bool IsFreeSession { get; set; } = false;
    public string? Notes { get; set; }

    public Seeker Seeker { get; set; } = default!;
    public Helper Helper { get; set; } = default!;
    public Review? Review { get; set; } // One-to-one relationship with Review
    public Payment? Payment { get; set; } // One-to-one relationship with Payment
    public ICollection<Message> Messages { get; set; } = []; // Messages related to this session (e.g., pre-session coordination, post-session follow-up, etc.)
}
