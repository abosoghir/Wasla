using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.TrustSafety;

// ÊãËá ØáÈÇÊ ÇáÊÍŞŞ ãä ÇáåæíÉ Ãæ ÇáãÚáæãÇÊ ÇáÃÎÑì ááãÓÊÎÏãíä
public class VerificationRequest : AuditableEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public VerificationType Type { get; set; }
    public VerificationStatus Status { get; set; } = VerificationStatus.Pending;
    public string? DocumentUrl { get; set; }
    public string? DocumentNumber { get; set; }
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Notes { get; set; }
    public string? ReviewedById { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? RejectionReason { get; set; } // İí ÍÇáÉ ÇáÑİÖ¡ íãßä Ãä íÍÊæí Úáì ÓÈÈ ÇáÑİÖ Ãæ ãáÇÍÙÇÊ ááãÓÊÎÏã Íæá ãÇ íÍÊÇÌ Åáì ÊÕÍíÍå Ãæ ÊŞÏíãå áÅÚÇÏÉ ÇáÊŞÏíã

    public ApplicationUser User { get; set; } = default!;
    public ApplicationUser? ReviewedBy { get; set; }
}
