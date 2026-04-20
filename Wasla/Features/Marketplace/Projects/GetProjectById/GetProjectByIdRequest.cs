namespace Wasla.Features.Marketplace.Projects.GetProjectById;

public record GetProjectByIdRequest(int Id) : IRequest<Result<GetProjectByIdResponse>>;

public record GetProjectByIdResponse(
    int Id, int SeekerId, string Title, string Description, decimal Budget, int DurationDays,
    ProjectCategory Category, ProjectStatus Status, string? RequiredSkills,
    DateTime CreatedOn, List<MilestoneResponse> Milestones, List<ProjectOfferResponse> Offers
);

public record MilestoneResponse(int Id, string Title, string Description, decimal Amount, int OrderIndex, MilestoneStatus Status, DateTime? DueDate);
public record ProjectOfferResponse(int Id, int HelperId, string? Message, decimal ProposedPrice, int ProposedDurationDays, OfferStatus Status);
