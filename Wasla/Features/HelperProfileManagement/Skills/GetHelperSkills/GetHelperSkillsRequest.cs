namespace Wasla.Features.HelperProfileManagement.Skills.GetHelperSkills;

public record GetHelperSkillsRequest(int HelperId) : IRequest<Result<List<HelperSkillDto>>>;

public record HelperSkillDto(
    int Id,
    int SkillId,
    string SkillName,
    ProficiencyLevel Proficiency
);
