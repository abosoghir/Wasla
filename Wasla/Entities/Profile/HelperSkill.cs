using Wasla.Common.Enums;

namespace Wasla.Entities.Profile;

public class HelperSkill : AuditableEntity
{
    public int Id { get; set; }
    public int HelperId { get; set; }
    public int SkillId { get; set; }

    public ProficiencyLevel Proficiency { get; set; }

    public Helper Helper { get; set; } = default!;
    public Skill Skill { get; set; } = default!;
}