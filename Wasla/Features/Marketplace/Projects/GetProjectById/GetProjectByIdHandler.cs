namespace Wasla.Features.Marketplace.Projects.GetProjectById;

public class GetProjectByIdHandler(IRepository<Project> projectRepo)
    : IRequestHandler<GetProjectByIdRequest, Result<GetProjectByIdResponse>>
{
    private readonly IRepository<Project> _projectRepo = projectRepo;

    public async Task<Result<GetProjectByIdResponse>> Handle(GetProjectByIdRequest request, CancellationToken ct)
    {
        var project = await _projectRepo
            .Include(p => p.Milestones)
            .Where(p => p.Id == request.Id && !p.IsDeleted)
            .Select(p => new GetProjectByIdResponse(
                p.Id, p.SeekerId, p.Title, p.Description, p.Budget, p.DurationDays,
                p.Category, p.Status, p.RequiredSkills, p.CreatedOn,
                p.Milestones.OrderBy(m => m.OrderIndex).Select(m => new MilestoneResponse(
                    m.Id, m.Title, m.Description, m.Amount, m.OrderIndex, m.Status, m.DueDate
                )).ToList(),
                p.Offers.Select(o => new ProjectOfferResponse(
                    o.Id, o.HelperId, o.Message, o.ProposedPrice, o.ProposedDurationDays, o.Status
                )).ToList()
            ))
            .FirstOrDefaultAsync(ct);

        if (project is null)
            return Result.Failure<GetProjectByIdResponse>(MarketplaceErrors.ProjectNotFound);

        return Result.Success(project);
    }
}
