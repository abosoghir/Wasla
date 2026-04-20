using Wasla.Features.Marketplace.ProjectMilestones.CreateMilestone;

namespace Wasla.Features.Marketplace.ProjectMilestones.CreateMilestone;

public record CreateMilestoneRequest(int ProjectId, string Title, string Description, decimal Amount, int OrderIndex, DateTime? DueDate)
    : IRequest<Result<CreateMilestoneResponse>>;


public class CreateMilestoneRequestValidator : AbstractValidator<CreateMilestoneRequest>
{
    public CreateMilestoneRequestValidator()
    {
        RuleFor(x => x.ProjectId).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.OrderIndex).GreaterThanOrEqualTo(0);
    }
}
