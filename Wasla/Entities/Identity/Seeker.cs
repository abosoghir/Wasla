using Wasla.Entities.Marketplace;
using SeekerTask = Wasla.Entities.Marketplace.SeekerTask;

namespace Wasla.Entities.Identity;

public class Seeker : AuditableEntity
{
    public int Id { get; set; } 
    public string UserId { get; set; } = string.Empty;

    public string? Location { get; set; }
    public string? CompanyName { get; set; }

    public int TotalTasksPosted { get; set; }
    public int TotalSessionsBooked { get; set; }
    public decimal TotalAmountSpent { get; set; }

    public ApplicationUser User { get; set; } = default!;
    public ICollection<SeekerTask> Tasks { get; set; } = [];
    public ICollection<Session> Sessions { get; set; } = [];
    public ICollection<Project> Projects { get; set; } = []; // Projects this seeker has posted
}
