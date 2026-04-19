namespace Wasla.Features.HelperProfileManagement.Skills.GetHelperSkills;

public class GetHelperSkillsHandler(IRepository<HelperSkill> helperSkillRepo)
    : IRequestHandler<GetHelperSkillsRequest, Result<List<HelperSkillDto>>>
{
    private readonly IRepository<HelperSkill> _helperSkillRepo = helperSkillRepo;

    public async Task<Result<List<HelperSkillDto>>> Handle(GetHelperSkillsRequest request, CancellationToken ct)
    {
        var skills = await _helperSkillRepo
            .Include(hs => hs.Skill)
            .Where(hs => hs.HelperId == request.HelperId && !hs.IsDeleted)
            .Select(hs => new HelperSkillDto(
                hs.Id, hs.SkillId, hs.Skill.Name, hs.Proficiency
            ))
            .ToListAsync(ct);

        return Result.Success(skills);
    }
}
