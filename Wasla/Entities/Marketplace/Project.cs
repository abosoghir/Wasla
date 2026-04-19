using Wasla.Common.Enums;
using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Marketplace;

public class Project : AuditableEntity
{
    public int Id { get; set; }
    public string SeekerId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public int DurationDays { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Draft;
    public ProjectCategory Category { get; set; }
    public string? RequiredSkills { get; set; }
    public bool IsPublic { get; set; } = true;

    public Seeker Seeker { get; set; } = default!;
    public ICollection<ProjectMilestone> Milestones { get; set; } = [];
    public ICollection<ProjectOffer> Offers { get; set; } = [];
    public ICollection<ProjectAttachment> Attachments { get; set; } = [];
}
