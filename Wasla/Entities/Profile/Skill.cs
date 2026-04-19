using Wasla.Entities.Common;
using Wasla.Entities.Identity;

namespace Wasla.Entities.Profile;

public class Skill : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<HelperSkill> HelperSkills { get; set; } = [];
    public ICollection<ProjectSkill> ProjectSkills { get; set; } = [];
}

