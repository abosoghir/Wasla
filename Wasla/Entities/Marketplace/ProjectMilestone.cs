using Wasla.Common.Enums;
using Wasla.Entities.Common;

namespace Wasla.Entities.Marketplace;

// devide the project into milestones, each milestone
// can have multiple deliverables, and each deliverable can be reviewed and approved separately
// . This allows for better tracking of progress and payments based on milestone completion.
public class ProjectMilestone : AuditableEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int OrderIndex { get; set; } //the order of the milestone in the project, starting from 0 for the first milestone
    public MilestoneStatus Status { get; set; } = MilestoneStatus.Pending;
    public DateTime? CompletedAt { get; set; }
    public DateTime? DueDate { get; set; }

    public Project Project { get; set; } = default!;
    // Each milestone can have multiple deliverables, and each deliverable can be reviewed and approved separately
    public ICollection<MilestoneDeliverable> Deliverables { get; set; } = []; 
}
