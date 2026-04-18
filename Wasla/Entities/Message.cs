using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class Message : AuditableEntity
{
    public int Id { get; set; } 

    public string SenderId { get; set; } = string.Empty;

    public string ReceiverId { get; set; } = string.Empty;

    public int? TaskId { get; set; }

    public int? SessionId { get; set; }

    public string Content { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ApplicationUser Sender { get; set; } = default!;
    public ApplicationUser Receiver { get; set; } = default!;
    public Task? Task { get; set; }
    public Session? Session { get; set; }
}
