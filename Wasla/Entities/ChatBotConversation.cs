using EduBrain.Entities.Users;

namespace Wasla.Entities;

/// <summary>
/// Free chatbot available to all users.
/// Stores conversation history as JSON.
/// </summary>
public class ChatBotConversation
{
    public int Id { get; set; } 

    public int? UserId { get; set; } // null = anonymous guest

    /// <summary>JSON array of { role, content, timestamp } objects.</summary>
    public string MessagesJson { get; set; } = "[]";

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ApplicationUser? User { get; set; }
}

