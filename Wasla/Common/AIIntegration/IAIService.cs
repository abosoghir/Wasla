using Wasla.Common.Enums;
using Wasla.Common.ResultPattern;

namespace Wasla.Common.AIIntegration;

public interface IAIService
{
    Task<Result<string>> CallAIFeatureAsync(AIFeatureType featureType, string input, CancellationToken ct);
}
