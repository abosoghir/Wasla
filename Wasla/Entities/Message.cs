using EduBrain.Entities.Users;

namespace Wasla.Entities;

public class Message
{
    public int Id { get; set; } 

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

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
