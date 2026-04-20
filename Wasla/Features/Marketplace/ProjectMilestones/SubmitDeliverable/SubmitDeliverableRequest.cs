using Wasla.Features.Marketplace.ProjectMilestones.SubmitDeliverable;

namespace Wasla.Features.Marketplace.ProjectMilestones.SubmitDeliverable;

public record SubmitDeliverableRequest(int MilestoneId, string FileName, string FileUrl, string? FileType, long FileSize, string? Description)
    : IRequest<Result<SubmitDeliverableResponse>>;

public class SubmitDeliverableRequestValidator : AbstractValidator<SubmitDeliverableRequest>
{
    public SubmitDeliverableRequestValidator()
    {
        RuleFor(x => x.MilestoneId).GreaterThan(0);
        RuleFor(x => x.FileName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.FileUrl).NotEmpty().MaximumLength(500);
        RuleFor(x => x.FileSize).GreaterThan(0);
    }
}
