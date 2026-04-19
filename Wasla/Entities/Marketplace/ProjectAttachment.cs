using Wasla.Entities.Common;

namespace Wasla.Entities.Marketplace;

public class ProjectAttachment : AuditableEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string? FileType { get; set; }
    public long FileSize { get; set; }
    public string? Description { get; set; }

    public Project Project { get; set; } = default!;
}
