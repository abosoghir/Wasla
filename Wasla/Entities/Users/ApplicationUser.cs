using Microsoft.AspNetCore.Identity;
using Wasla.Entities;
using Wasla.Entities.Users;

namespace EduBrain.Entities.Users;

public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Id = Guid.CreateVersion7().ToString();
        SecurityStamp = Guid.CreateVersion7().ToString();
    }
    public string Name { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    
    public bool IsBlocked { get; set; } = false;
    public List<RefreshToken> RefreshTokens { get; set; } = [];

    // Navigation properties
    public Seeker? Seeker { get; set; }
    public Helper? Helper { get; set; }
    public ICollection<Message> SentMessages { get; set; } = [];
    public ICollection<Message> ReceivedMessages { get; set; } = [];
    public ICollection<Notification> Notifications { get; set; } = [];
    public ICollection<AIUsage> AIUsages { get; set; } = [];
    public ICollection<ChatBotConversation> ChatBotConversations { get; set; } = [];

}
