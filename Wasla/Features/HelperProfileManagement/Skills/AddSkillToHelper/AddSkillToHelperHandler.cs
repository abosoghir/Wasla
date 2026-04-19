
namespace Wasla.Features.HelperProfileManagement.Skills.AddSkillToHelper;

public class AddSkillToHelperHandler(
    IRepository<Helper> helperRepo,
    IRepository<HelperSkill> helperSkillRepo,
    IRepository<Skill> skillRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AddSkillToHelperRequest, Result<AddSkillToHelperResponse>>
{
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IRepository<HelperSkill> _helperSkillRepo = helperSkillRepo;
    private readonly IRepository<Skill> _skillRepo = skillRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<AddSkillToHelperResponse>> Handle(AddSkillToHelperRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null)
            return Result.Failure<AddSkillToHelperResponse>(HelperProfileErrors.HelperNotFound);

        var skill = await _skillRepo.GetByIdAsync(request.SkillId, ct);
        if (skill is null)
            return Result.Failure<AddSkillToHelperResponse>(HelperProfileErrors.SkillNotFound);

        var alreadyAdded = await _helperSkillRepo.AnyAsync(
            hs => hs.HelperId == helper.Id && hs.SkillId == request.SkillId, ct);

        if (alreadyAdded)
            return Result.Failure<AddSkillToHelperResponse>(HelperProfileErrors.SkillAlreadyAdded);

        var helperSkill = new HelperSkill
        {
            HelperId = helper.Id,
            SkillId = request.SkillId,
            Proficiency = request.Proficiency,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _helperSkillRepo.AddAsync(helperSkill, ct);
        await _helperSkillRepo.SaveChangesAsync(ct);

        return Result.Success(new AddSkillToHelperResponse(helperSkill.Id));
    }
}
