using Wasla.Entities.Common;

namespace Wasla.Entities.Marketplace;

public class TaskAttachment : AuditableEntity
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string? FileType { get; set; }
    public long FileSize { get; set; }
    public string UploadedById { get; set; } = string.Empty; // Could be Seeker or Helper

    public SeekerTask Task { get; set; } = default!;
}
