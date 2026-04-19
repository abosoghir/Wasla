using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.TrustSafety;

// ÇÊ Ãæ ÊŞííã ÊÌÑÈÉ ãÚ ãÓÊÎÏãíä Ãæ ÎÏãÇÊ Ãæ ãÔÇÑíÚ Ãæ ÌáÓÇÊ
public class Review : AuditableEntity
{
    public int Id { get; set; }
    public string ReviewerId { get; set; } = string.Empty;
    public string RevieweeId { get; set; } = string.Empty;

    public ReviewType Type { get; set; }
    public int? RelatedEntityId { get; set; } // ãÚÑİ ÇáßíÇä ÇáãÊÚáŞ ÈÇáÊŞííã (ãËá ãÚÑİ ÇáãåãÉ Ãæ ÇáãÔÑæÚ Ãæ ÇáÌáÓÉ Ãæ ÇáÎÏãÉ)
    public string? RelatedEntityType { get; set; } // äæÚ ÇáßíÇä ÇáãÊÚáŞ ÈÇáÊŞííã (ãËá "Task", "Project", "Session", "Service")

    public int Rating { get; set; } // ÊŞííã ÚÇã ãä 1 Åáì 5
    public string? Comment { get; set; }
    public bool IsVisible { get; set; } = true; // íãßä ááãÓÊÎÏãíä ÇÎÊíÇÑ ÌÚá ÊŞííãÇÊåã ÎÇÕÉ Ãæ ÚÇãÉ¡ Ãæ íãßä Ãä íÊã ÇáÊÍßã İí Ğáß ãä ŞÈá ÇáäÙÇã ÈäÇÁğ Úáì ÓíÇÓÇÊ ãÚíäÉ
    public bool IsVerified { get; set; } = false; // íãßä Ãä íÊã ÇáÊÍŞŞ ãä ÕÍÉ ÇáÊŞííã ãä ŞÈá İÑíŞ ÇáÏÚã Ãæ ÇáäÙÇã

    public int? QualityRating { get; set; } // ÊŞííã ÌæÏÉ ÇáÎÏãÉ Ãæ ÇáÊÌÑÈÉ
    public int? CommunicationRating { get; set; } // ÊŞííã ÇáÊæÇÕá æÇáÊİÇÚá ãÚ ÇáØÑİ ÇáÂÎÑ
    public int? TimelinessRating { get; set; } // ÊŞííã ÇáÇáÊÒÇã ÈÇáãæÇÚíÏ æÇáÌÏæá ÇáÒãäí ÇáãÊİŞ Úáíå
    public int? ValueRating { get; set; } // ÊŞííã ÇáŞíãÉ ãŞÇÈá ÇáãÇá Ãæ ÇáÌåÏ ÇáãÈĞæá

    public ApplicationUser Reviewer { get; set; } = default!;
    public ApplicationUser Reviewee { get; set; } = default!;
}
