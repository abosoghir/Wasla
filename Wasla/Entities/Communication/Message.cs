using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;
using Wasla.Entities.Marketplace;

namespace Wasla.Entities.Communication;

public class Message : AuditableEntity
{
    public int Id { get; set; }
    public string SenderId { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = string.Empty;

    public MessageType Type { get; set; } = MessageType.Text;
    public string Content { get; set; } = string.Empty;
    public string? AttachmentUrl { get; set; } // URL to the attached file (if any)
    public string? FileName { get; set; } // Original file name of the attachment (if any)

    public int? TaskId { get; set; }
    public int? ProjectId { get; set; }
    public int? SessionId { get; set; }

    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public bool IsDeletedBySender { get; set; } = false;
    public bool IsDeletedByReceiver { get; set; } = false;

    public int? ReplyToId { get; set; }
    public Message? ReplyTo { get; set; } // Reference to the original message being replied to (if any)
    public ICollection<Message> Replies { get; set; } = []; // Collection of messages that are replies to this message

    public ApplicationUser Sender { get; set; } = default!;
    public ApplicationUser Receiver { get; set; } = default!;
    public Marketplace.SeekerTask? Task { get; set; }
    public Project? Project { get; set; }
    public Session? Session { get; set; }
}
