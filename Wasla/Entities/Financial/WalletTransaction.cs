using Wasla.Common.Enums;
using Wasla.Entities.Common;

namespace Wasla.Entities.Financial;

public class WalletTransaction : AuditableEntity
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.Completed;
    public string? ReferenceNumber { get; set; } // Optional: Reference number from the payment gateway or internal transaction ID
    public string? Description { get; set; } // Optional: A brief description of the transaction (e.g., "Deposit from PayPal", "Payment for Project #123", "Refund for Session #456")
    public int? RelatedEntityId { get; set; } // Optional: The ID of the related entity (e.g., TaskId, ProjectId, SessionId, ServiceId, SubscriptionId)
    public string? RelatedEntityType { get; set; } // Optional: The type of the related entity (e.g., "Task", "Project", "Session", "Service", "Subscription")
    public decimal BalanceAfter { get; set; }  // The wallet balance after this transaction was applied, for historical accuracy and auditing purposes

    public Wallet Wallet { get; set; } = default!;
}
