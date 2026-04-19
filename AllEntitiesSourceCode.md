# Wasla Entities Source Code


## AIUsage.cs


```csharp

namespace Wasla.Entities;

/// <summary>
/// Tracks AI-powered feature usage — paid with points (400+ pts threshold)
/// or via direct subscription. Feature types: ProfileEnhancement, CodeReview, etc.
/// </summary>
public class AIUsage : AuditableEntity
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string FeatureType { get; set; } = string.Empty;

    public int PointsSpent { get; set; } = 0;

    public string? InputData { get; set; }

    public string? OutputData { get; set; }

    public DateTime UsedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ApplicationUser User { get; set; } = null!;
}
```


## ChatBotConversation.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

/// <summary>
/// Free chatbot available to all users.
/// Stores conversation history as JSON.
/// </summary>
public class ChatBotConversation : AuditableEntity
{
    public int Id { get; set; } 

    public string? UserId { get; set; } // null = anonymous guest

    /// <summary>JSON array of { role, content, timestamp } objects.</summary>
    public string MessagesJson { get; set; } = "[]";

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ApplicationUser? User { get; set; }
}


```


## HelperProject.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class HelperProject : AuditableEntity
{
    public int Id { get; set; }
    public int HelperId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string? ProjectImageUrl { get; set; } 
    public string? RepositoryUrl { get; set; }
    public string? LiveDemoUrl { get; set; }

    // Navigation properties
    public Helper Helper { get; set; } = default!;
}

```


## HelperServices.cs


```csharp

namespace Wasla.Entities;

public class HelperServices : AuditableEntity
{
    public int Id { get; set; } 

    public int HelperId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty; // e.g. "Frontend", "Backend", "Math", "Design"

    public decimal Price { get; set; } 

    // Navigation properties
    public Helper Helper { get; set; } = default!;
}

```


## Message.cs


```csharp

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

```


## Notification.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

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
```


## Payment.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class Payment : AuditableEntity
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public int? TaskId { get; set; }

    public string PayerId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public string? TransactionRef { get; set; }

    public DateTime PaidAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Session? Session { get; set; }
    public Task? Task { get; set; }
    public ApplicationUser Payer { get; set; } = default!;
}

public enum PaymentStatus
{
    Pending = 0,
    Completed = 1,
    Failed = 2,
    Refunded = 3
}

```


## PointTransaction.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class PointTransaction : AuditableEntity
{
    public int Id { get; set; }

    public int HelperId { get; set; }

    public PointTransactionType Type { get; set; }

    public int Points { get; set; }
    public string? Reason { get; set; }


    // Navigation properties
    public Helper Helper { get; set; } = default!;
}


public enum PointTransactionType
{
    Earned = 0,   // task completed
    Spent = 1,    // used for AI feature
    Bonus = 2,    // platform reward
    Deducted = 3 // penalty / refund
}
```


## Review.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class Review : AuditableEntity
{
    public int Id { get; set; } 

    public int? TaskId { get; set; }

    public int? SessionId { get; set; }

    public string ReviewerId { get; set; } = string.Empty;

    public string RevieweeId { get; set; } = string.Empty;

    public int Rating { get; set; }

    public string? Comment { get; set; }


    // Navigation properties
    public Task? Task { get; set; }
    public Session? Session { get; set; }
    public ApplicationUser Reviewer { get; set; } = default!;
    public ApplicationUser Reviewee { get; set; } = default!;
}

```


## Session.cs


```csharp

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
```


## Task.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class Task : AuditableEntity
{
    public int Id { get; set; }
    public int SeekerId { get; set; }

    public int? HelperId { get; set; } 

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty; // e.g. "WebBackend", "WebFrontend", "Math"

    public TaskStatus Status { get; set; } = TaskStatus.Open;

    public decimal? Budget { get; set; }

    public int PointsAwarded { get; set; } = 10;

    public bool IsFreeTask { get; set; } = false;

    public DateTime? CompletedAt { get; set; }

    public decimal ServicesFee { get; set; } = 0.10m; // 10% fee taken by Wasla from the helper's earnings

    public decimal TotalCost => Budget.HasValue ? Budget.Value * (1 + ServicesFee) : 0m;

    // Navigation properties
    public Seeker Seeker { get; set; } = default!;
    public Helper? Helper { get; set; }
    public ICollection<TaskOffer> Offers { get; set; } = [];
    public Review? Review { get; set; }
    public Payment? Payment { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}

public enum TaskStatus
{
    Open = 0,
    InProgress = 1,
    UnderReview = 2,
    Completed = 3,
    Cancelled = 4
}
```


## TaskOffer.cs


```csharp

using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities;

public class TaskOffer : AuditableEntity
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int HelperId { get; set; }

    public string? Message { get; set; }

    public decimal ProposedPrice { get; set; }

    public TaskOfferStatus Status { get; set; } = TaskOfferStatus.Pending;

    // Navigation properties
    public Task Task { get; set; } = default!;
    public Helper Helper { get; set; } = default!;
}

public enum TaskOfferStatus
{
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
    Withdrawn = 3
}

```


## Common\AuditableEntity.cs


```csharp

using Wasla.Entities.Identity;

namespace Wasla.Entities.Common;

public class AuditableEntity
{
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string? UpdatedById { get; set; }
    public DateTime? UpdatedOn { get; set; }

    public bool IsDeleted { get; set; }

    // Navigation 
    public ApplicationUser CreatedBy { get; set; } = default!;
    public ApplicationUser? UpdatedBy { get; set; }
}

```


## Identity\ApplicationRole.cs


```csharp

using Microsoft.AspNetCore.Identity;

namespace Wasla.Entities.Identity;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
        Id = Guid.CreateVersion7().ToString();
    }
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
}

```


## Identity\ApplicationUser.cs


```csharp

using Microsoft.AspNetCore.Identity;

namespace Wasla.Entities.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Id = Guid.CreateVersion7().ToString();
        SecurityStamp = Guid.CreateVersion7().ToString();
    }
    public string Name { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }

    public string? EmailConfirmationCode { get; set; }
    public DateTime? EmailConfirmationCodeExpiration { get; set; }
    public string? ResetPasswordCode { get; set; }
    public DateTime? ResetPasswordCodeExpiration { get; set; }

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

```


## Identity\Helper.cs


```csharp

using Wasla.Entities.Common;

namespace Wasla.Entities.Identity;

public class Helper : AuditableEntity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;

    // Profile
    public string? Bio { get; set; }
    public decimal HourlyRate { get; set; }
    public bool IsAvailable { get; set; } = true;

    // Points & Rewards
    public int Points { get; set; }
    public int CompletedTasksCount { get; set; }
    public double SpeedOfResponseInMintues { get; set; } // Average time (in hours) to respond to offers or messages
    
    // Every 5th task is free (gifted) — checked in service layer
    public bool IsNextTaskFree => CompletedTasksCount > 0 && CompletedTasksCount % 4 == 0;

    // Rating
    public double AverageRating { get; set; }
    public int TotalReviewsCount { get; set; }

    public decimal TotalEarnings { get; set; }

    // Navigation
    public ApplicationUser User { get; set; } = default!;
    public ICollection<HelperServices> Services { get; set; } = [];
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<TaskOffer> TaskOffers { get; set; } = [];
    public ICollection<Review> ReviewsReceived { get; set; } = [];
    public ICollection<PointTransaction> PointTransactions { get; set; } = [];
    public ICollection<HelperProject> Projects { get; set; } = [];
}

```


## Identity\RefreshToken.cs


```csharp

namespace Wasla.Entities.Identity;

//[Owned]
public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public string JwtId { get; set; } = string.Empty;  // Bind refresh token to a specific JWT
    public DateTime ExpiresOn { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedOn { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    public bool IsActive => RevokedOn is null && !IsExpired;
}
```


## Identity\Seeker.cs


```csharp

using Wasla.Entities.Common;

namespace Wasla.Entities.Identity;

public class Seeker : AuditableEntity
{
    public string Id { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    // Profile
    public string? Bio { get; set; }

    // Stats
    public int TotalTasksPosted { get; set; }
    public int TotalSessionsBooked { get; set; }
    public decimal TotalAmountSpent { get; set; }

    // Navigation
    public ApplicationUser User { get; set; } = default!;
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<Review> ReviewsGiven { get; set; } = [];

}

```

