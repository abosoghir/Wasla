using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Financial;

// the operation of transferring money from one user to another,
// or from a user to the platform (e.g., for fees), or from the platform to a user
// (e.g., for refunds or payouts). It can be associated with various purposes like task payments,
// project payments, session payments, service payments, or subscription payments. It also tracks the payment
// method used, the status of the payment, and any related transaction references or gateway responses.
public class Payment : AuditableEntity
{
    public int Id { get; set; }
    public string PayerId { get; set; } = string.Empty; // The user who initiates the payment (could be a seeker or helper)
    public string? PayeeId { get; set; } // The user who receives the payment (could be a helper, or null if it's a payment to the platform)
    public decimal Amount { get; set; }
    public decimal PlatformFee { get; set; } // The fee charged by the platform for facilitating the payment
    public decimal NetAmount => Amount - PlatformFee; // The amount that the payee will receive after deducting the platform fee

    public PaymentPurpose Purpose { get; set; }
    public int? RelatedEntityId { get; set; } // The ID of the related entity (e.g., TaskId, ProjectId, SessionId, ServiceId, SubscriptionId)
    public string? RelatedEntityType { get; set; } // The type of the related entity (e.g., "Task", "Project", "Session", "Service", "Subscription")

    public PaymentMethod Method { get; set; } = PaymentMethod.Wallet;
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? TransactionReference { get; set; } // Reference number from the payment gateway or internal transaction ID
    public string? GatewayResponse { get; set; } // Store the raw response from the payment gateway for auditing and troubleshooting
    public DateTime? PaidAt { get; set; }

    public ApplicationUser Payer { get; set; } = default!;
    public ApplicationUser? Payee { get; set; }
}
