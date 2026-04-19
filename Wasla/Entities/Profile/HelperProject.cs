using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Profile;

public class HelperProject : AuditableEntity
{
    public int Id { get; set; }
    public int HelperId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ProjectImageUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? LiveDemoUrl { get; set; } // Optional field for a live demo link

    public ICollection<ProjectSkill> ProjectSkills { get; set; } = [];
    public Helper Helper { get; set; } = default!;
}
