using Wasla.Entities.Common;

namespace Wasla.Entities.Marketplace;

public class MilestoneDeliverable : AuditableEntity
{
    public int Id { get; set; }
    public int MilestoneId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty; // this can be a link to the file stored in cloud storage (e.g., AWS S3, Azure Blob Storage) or a local path
    public string? FileType { get; set; }
    public long FileSize { get; set; }
    public string? Description { get; set; }

    public ProjectMilestone Milestone { get; set; } = default!;
}
