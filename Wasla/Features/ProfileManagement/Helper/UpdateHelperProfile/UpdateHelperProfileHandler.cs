namespace Wasla.Features.ProfileManagement.Helper.UpdateHelperProfile;

public class UpdateHelperProfileHandler(
    IRepository<Wasla.Entities.Identity.Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateHelperProfileRequest, Result>
{
    private readonly IRepository<Wasla.Entities.Identity.Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(UpdateHelperProfileRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == null)
            return Result.Failure(ProfileErrors.UserNotFound);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper == null)
            return Result.Failure(ProfileErrors.HelperProfileNotFound);

        if (request.Headline != null) helper.Headline = request.Headline;
        if (request.Location != null) helper.Location = request.Location;
        if (request.HourlyRate.HasValue) helper.HourlyRate = request.HourlyRate.Value;
        if (request.IsAvailable.HasValue) helper.IsAvailable = request.IsAvailable.Value;

        helper.UpdatedById = userId!;
        helper.UpdatedOn = DateTime.UtcNow;

        await _helperRepo.UpdateAsync(helper, ct);
        await _helperRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
