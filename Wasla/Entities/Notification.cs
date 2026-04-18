using EduBrain.Entities.Common;
using EduBrain.Entities.Users;

namespace Wasla.Entities;

public class Notification : AuditableEntity
{
    public int Id { get; set; } 

    public string UserId { get; set; } = string.Empty;

    public NotificationType Type { get; set; } // e.g. "NewOffer", "SessionConfirmed", "TaskCompleted"

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;


    // Navigation properties
    public ApplicationUser User { get; set; } = default!;
}

public enum NotificationType
{
    NewOffer = 1,
    SessionConfirmed = 2,
    TaskCompleted = 3,
    MessageReceived = 4,
    PaymentSuccess = 5
}