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