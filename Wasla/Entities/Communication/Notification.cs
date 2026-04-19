using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Communication;

// to tell the user about important events like new offers, session confirmations, payment updates, etc.
public class Notification : AuditableEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string? ActionUrl { get; set; } // URL to navigate when the notification is clicked (if any)
    public string? RelatedEntityType { get; set; } // e.g., "Session", "Offer", "Payment", etc. (if any)
    public int? RelatedEntityId { get; set; } // ID of the related entity (if any)
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    // Priority can be used to determine the order of notifications or to trigger different handling logic (e.g., send email for high priority)
    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal; 
    public NotificationChannel Channel { get; set; } = NotificationChannel.InApp;
    public bool IsSent { get; set; } = false;
    public DateTime? SentAt { get; set; }

    public ApplicationUser User { get; set; } = default!;
}
