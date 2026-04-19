namespace Wasla.Entities.Profile;

public class ProjectSkill : AuditableEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int SkillId { get; set; }

    public HelperProject Project { get; set; } = default!;
    public Skill Skill { get; set; } = default!;
}
