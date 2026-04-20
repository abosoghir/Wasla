namespace Wasla.Features.AIIntegration.UseAIFeature;

public record UseAIFeatureRequest(AIFeatureType FeatureType, string Input) : IRequest<Result<UseAIFeatureResponse>>;

public record UseAIFeatureResponse(string Result, int PointsUsed, AIRequestStatus Status);

public class UseAIFeatureRequestValidator : AbstractValidator<UseAIFeatureRequest>
{
    public UseAIFeatureRequestValidator()
    {
        RuleFor(x => x.FeatureType).IsInEnum();
        RuleFor(x => x.Input).NotEmpty().MaximumLength(10000);
    }
}
