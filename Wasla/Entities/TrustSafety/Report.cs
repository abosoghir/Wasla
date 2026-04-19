using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.TrustSafety;

// ÊãËá ÈáÇÛÇÊ ÇáãÓÊÎÏãíä Úä ãÎÇáİÇÊ Ãæ ãÔÇßá İí ÇáãäÕÉ
public class Report : AuditableEntity
{
    public int Id { get; set; }
    public string ReporterId { get; set; } = string.Empty; // ÇáãÓÊÎÏã ÇáĞí ŞÏã ÇáÈáÇÛ
    public string? ReportedUserId { get; set; } // ÇáãÓÊÎÏã ÇáĞí Êã ÇáÅÈáÇÛ Úäå (ÇÎÊíÇÑí¡ ŞÏ íßæä ÇáÈáÇÛ Úä ãÍÊæì Ãæ ÎÏãÉ æáíÓ ãÓÊÎÏã)
    public ReportType Type { get; set; } // äæÚ ÇáÈáÇÛ (ãËá Úä ãÓÊÎÏã¡ ãåãÉ¡ ãÔÑæÚ¡ ÑÓÇáÉ¡ ÊÚáíŞ¡ ÊŞííã¡ ÏİÚ¡ ÂÎÑ)
    public int? RelatedEntityId { get; set; } // ãÚÑİ ÇáßíÇä ÇáãÊÚáŞ ÈÇáÈáÇÛ (ãËá ãÚÑİ ÇáãåãÉ Ãæ ÇáãÔÑæÚ Ãæ ÇáÑÓÇáÉ Ãæ ÇáÊÚáíŞ)
    public string? RelatedEntityType { get; set; } // äæÚ ÇáßíÇä ÇáãÊÚáŞ ÈÇáÈáÇÛ (ãËá "Task", "Project", "Message", "Comment")
    public string Reason { get; set; } = string.Empty;
    public string? Details { get; set; }
    public string? EvidenceUrls { get; set; } // íãßä Ãä íÍÊæí Úáì ÑæÇÈØ Åáì ÕæÑ Ãæ ãÓÊäÏÇÊ Ãæ áŞØÇÊ ÔÇÔÉ ÊÏÚã ÇáÈáÇÛ
    public ReportStatus Status { get; set; } = ReportStatus.Pending;
    public string? Resolution { get; set; } // ÊİÇÕíá ÇáÍá Ãæ ÇáŞÑÇÑ ÇáãÊÎĞ ÈÔÃä ÇáÈáÇÛ
    public string? ResolvedById { get; set; } // ÇáãÓÊÎÏã ÇáĞí ŞÇã ÈÍá ÇáÈáÇÛ
    public DateTime? ResolvedAt { get; set; } // ÊÇÑíÎ Íá ÇáÈáÇÛ
    public ReportSeverity Severity { get; set; } = ReportSeverity.Medium;

    public ApplicationUser Reporter { get; set; } = default!;
    public ApplicationUser? ReportedUser { get; set; }
    public ApplicationUser? ResolvedBy { get; set; }
}
