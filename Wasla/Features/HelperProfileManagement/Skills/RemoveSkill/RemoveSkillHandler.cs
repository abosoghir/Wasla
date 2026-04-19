namespace Wasla.Features.HelperProfileManagement.Skills.RemoveSkill;

public class RemoveSkillHandler(
    IRepository<HelperSkill> helperSkillRepo,
    IRepository<Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<RemoveSkillRequest, Result>
{
    private readonly IRepository<HelperSkill> _helperSkillRepo = helperSkillRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(RemoveSkillRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var helperSkill = await _helperSkillRepo.GetByIdAsync(request.Id, ct);
        if (helperSkill is null)
            return Result.Failure(HelperProfileErrors.HelperSkillNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper is null || helperSkill.HelperId != helper.Id)
            return Result.Failure(HelperProfileErrors.Unauthorized);

        await _helperSkillRepo.Delete(helperSkill);
        await _helperSkillRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
