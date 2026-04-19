

namespace Wasla.Entities.Marketplace;

public class SeekerTask : AuditableEntity
{
    public int Id { get; set; }
    public int SeekerId { get; set; }
    public int? HelperId { get; set; } // Nullable until an offer is accepted

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskCategory Category { get; set; }
    public Wasla.Common.Enums.TaskStatus Status { get; set; } = Wasla.Common.Enums.TaskStatus.Open;

    public decimal? Budget { get; set; }
    public int PointsAwarded { get; set; } = 10;
    public bool IsFreeTask { get; set; } = false;

    public DateTime? CompletedAt { get; set; }
    public DateTime? Deadline { get; set; }

    public decimal PlatformFee { get; set; } = 0.05m;
    public decimal? FinalPrice { get; set; }

    public Seeker Seeker { get; set; } = default!;
    public Helper? Helper { get; set; }
    public ICollection<TaskOffer> Offers { get; set; } = [];
    public Review? Review { get; set; } 
    public Payment? Payment { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
    public ICollection<TaskAttachment> Attachments { get; set; } = [];
}
