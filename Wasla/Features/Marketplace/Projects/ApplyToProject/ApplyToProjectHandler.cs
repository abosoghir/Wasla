namespace Wasla.Features.Marketplace.Projects.ApplyToProject;

public class ApplyToProjectHandler(
    IRepository<Project> projectRepo,
    IRepository<ProjectOffer> offerRepo,
    IRepository<Helper> helperRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<ApplyToProjectRequest, Result<ApplyToProjectResponse>>
{
    private readonly IRepository<Project> _projectRepo = projectRepo;
    private readonly IRepository<ProjectOffer> _offerRepo = offerRepo;
    private readonly IRepository<Helper> _helperRepo = helperRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<ApplyToProjectResponse>> Handle(ApplyToProjectRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        if (userId == null)
            return Result.Failure<ApplyToProjectResponse>(MarketplaceErrors.Unauthorized);

        var helper = await _helperRepo.FindAsync(h => h.UserId == userId, ct);
        if (helper == null)
            return Result.Failure<ApplyToProjectResponse>(MarketplaceErrors.HelperNotFound);

        var project = await _projectRepo.FindAsync(p => p.Id == request.ProjectId && !p.IsDeleted, ct);
        if (project == null)
            return Result.Failure<ApplyToProjectResponse>(MarketplaceErrors.ProjectNotFound);

        // Check if already applied
        var alreadyApplied = await _offerRepo.AnyAsync(
            o => o.ProjectId == request.ProjectId && o.HelperId == helper.Id && !o.IsDeleted, ct);
        if (alreadyApplied)
            return Result.Failure<ApplyToProjectResponse>(MarketplaceErrors.AlreadyOffered);

        var offer = new ProjectOffer
        {
            ProjectId = request.ProjectId,
            HelperId = helper.Id,
            Message = request.Message,
            ProposedPrice = request.ProposedPrice,
            ProposedDurationDays = request.ProposedDurationDays
        };

        await _offerRepo.AddAsync(offer, ct);
        await _offerRepo.SaveChangesAsync(ct);

        return Result.Success(new ApplyToProjectResponse(offer.Id));
    }
}
