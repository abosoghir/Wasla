using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Financial;
// the Wallet entity represents a user's financial account within the platform,
// tracking their balance, total deposits, total withdrawals, and the currency they use.
// It also maintains a collection of related wallet transactions for auditing and historical purposes.
// The Wallet is linked to the ApplicationUser entity to associate it with a specific user.
public class Wallet : AuditableEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public CurrencyType Currency { get; set; } = CurrencyType.EGP;
    public decimal Balance { get; set; }
    public decimal TotalDeposited { get; set; } // Cumulative total of all deposits made into the wallet
    public decimal TotalWithdrawn { get; set; } // Cumulative total of all withdrawals made from the wallet
    public bool IsActive { get; set; } = true;

    public ApplicationUser User { get; set; } = default!;
    public ICollection<WalletTransaction> Transactions { get; set; } = [];
}