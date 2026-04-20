namespace Wasla.Features.HelperBrowse.GetHelpers;

public class GetHelpersHandler(IRepository<Helper> helperRepo)
    : IRequestHandler<GetHelpersRequest, Result<GetHelpersResponse>>
{
    private readonly IRepository<Helper> _helperRepo = helperRepo;

    public async Task<Result<GetHelpersResponse>> Handle(GetHelpersRequest request, CancellationToken ct)
    {
        var query = _helperRepo.GetAll()
            .Include(h => h.User)
            .Include(h => h.HelperSkills)
                .ThenInclude(hs => hs.Skill)
            .Where(h => !h.IsDeleted);

        // Search filter
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(h =>
                h.User.Name.ToLower().Contains(search) ||
                (h.Headline != null && h.Headline.ToLower().Contains(search)) ||
                h.HelperSkills.Any(s => s.Skill.Name.ToLower().Contains(search))
            );
        }

        // Category filter
        if (request.Category.HasValue)
        {
            query = query.Where(h =>
                h.Services.Any(s => s.Category == request.Category.Value)
            );
        }

        // Rating filter
        if (request.MinRating.HasValue)
            query = query.Where(h => h.AverageRating >= request.MinRating.Value);

        // Price range filter
        if (request.MinPrice.HasValue)
            query = query.Where(h => h.HourlyRate >= request.MinPrice.Value);

        if (request.MaxPrice.HasValue)
            query = query.Where(h => h.HourlyRate <= request.MaxPrice.Value);

        // Availability filter
        if (request.AvailableOnly == true)
            query = query.Where(h => h.IsAvailable);

        // Verified filter
        if (request.VerifiedOnly == true)
            query = query.Where(h => h.IsVerified);

        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "rating" => query.OrderByDescending(h => h.AverageRating),
            "reviews" => query.OrderByDescending(h => h.TotalReviewsCount),
            "price_asc" => query.OrderBy(h => h.HourlyRate),
            "price_desc" => query.OrderByDescending(h => h.HourlyRate),
            _ => query.OrderByDescending(h => h.AverageRating * h.TotalReviewsCount) // Best match
        };

        var paginatedList = await PaginatedList<HelperListResponse>.CreateAsync(
            query.Select(h => new HelperListResponse(
                h.Id,
                h.UserId,
                h.User.Name,
                h.User.ProfilePictureUrl,
                h.Headline,
                h.Location,
                h.HourlyRate,
                h.IsAvailable,
                h.IsVerified,
                h.AverageRating,
                h.TotalReviewsCount,
                h.CompletedTasksCount,
                h.HelperSkills.Select(s => s.Skill.Name).ToList()
            )),
            request.PageNumber,
            request.PageSize,
            ct
        );

        var response = new GetHelpersResponse(
            paginatedList.Items,
            new PaginationResponse
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = request.PageSize,
                TotalPages = paginatedList.TotalPages,
                HasNextPage = paginatedList.HasNextPage
            }
        );

        return Result.Success(response);
    }
}
