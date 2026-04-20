namespace Wasla.Features.ProfileManagement.Seeker.UpdateSeekerProfile;

public class UpdateSeekerProfileHandler(
    IRepository<Wasla.Entities.Identity.Seeker> seekerRepo,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateSeekerProfileRequest, Result>
{
    private readonly IRepository<Wasla.Entities.Identity.Seeker> _seekerRepo = seekerRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(UpdateSeekerProfileRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == null)
            return Result.Failure(ProfileErrors.UserNotFound);

        var seeker = await _seekerRepo.FindAsync(s => s.UserId == userId, ct);
        if (seeker == null)
            return Result.Failure(ProfileErrors.SeekerProfileNotFound);

        seeker.Location = request.Location;
        seeker.CompanyName = request.CompanyName;
        seeker.UpdatedById = userId!;
        seeker.UpdatedOn = DateTime.UtcNow;

        await _seekerRepo.UpdateAsync(seeker, ct);
        await _seekerRepo.SaveChangesAsync(ct);

        return Result.Success();
    }
}
