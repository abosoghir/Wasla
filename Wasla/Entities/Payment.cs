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
