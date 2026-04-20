namespace Wasla.Features.HelperBrowse.GetHelperById;

public class GetHelperByIdHandler(IRepository<Helper> helperRepo)
    : IRequestHandler<GetHelperByIdRequest, Result<GetHelperByIdResponse>>
{
    private readonly IRepository<Helper> _helperRepo = helperRepo;

    public async Task<Result<GetHelperByIdResponse>> Handle(GetHelperByIdRequest request, CancellationToken ct)
    {
        var helper = await _helperRepo.GetAll()
            .Include(h => h.User)
            .Include(h => h.HelperSkills).ThenInclude(hs => hs.Skill)
            .Include(h => h.Services)
            .Include(h => h.Projects)
            .Include(h => h.ReviewsReceived).ThenInclude(r => r.Reviewer)
            .Where(h => h.Id == request.Id && !h.IsDeleted)
            .FirstOrDefaultAsync(ct);

        if (helper == null)
            return Result.Failure<GetHelperByIdResponse>(HelperBrowseErrors.HelperNotFound);

        var response = new GetHelperByIdResponse(
            helper.Id,
            helper.UserId,
            helper.User.Name,
            helper.User.ProfilePictureUrl,
            helper.User.Bio,
            helper.Headline,
            helper.Location,
            helper.HourlyRate,
            helper.IsAvailable,
            helper.IsVerified,
            helper.AverageRating,
            helper.TotalReviewsCount,
            helper.CompletedTasksCount,
            helper.CompletedProjectsCount,
            helper.CompletedSessionsCount,
            helper.TotalEarnings,
            helper.Points,
            helper.SpeedOfResponseInMintues,
            helper.CreatedOn,
            helper.HelperSkills.Select(s => s.Skill.Name).ToList(),
            helper.Services.Where(s => !s.IsDeleted).Select(s => new HelperServiceResponse(
                s.Id, s.Title, s.Description, s.Category, s.Price, s.DeliveryDays
            )).ToList(),
            helper.Projects.Where(p => !p.IsDeleted).Select(p => new HelperPortfolioResponse(
                p.Id, p.Title, p.Description, p.ProjectImageUrl
            )).ToList(),
            helper.ReviewsReceived.OrderByDescending(r => r.CreatedOn).Take(20).Select(r => new HelperReviewResponse(
                r.Id, r.Reviewer.Name, r.Reviewer.ProfilePictureUrl, r.Rating, r.Comment, r.Type, r.CreatedOn
            )).ToList()
        );

        return Result.Success(response);
    }
}
