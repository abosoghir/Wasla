using Wasla.Common.Enums;

namespace Wasla.Common.AIIntegration;

public static class AIPointsCost
{
    private static readonly Dictionary<AIFeatureType, int> Costs = new()
    {
        { AIFeatureType.ChatAssistant, 0 },
        { AIFeatureType.CVImprovement, 20 },
        { AIFeatureType.FileSummarization, 10 },
        { AIFeatureType.ProfileEnhancement, 15 },
        { AIFeatureType.ProposalGeneration, 25 }
    };

    public static int GetCost(AIFeatureType featureType)
    {
        return Costs.TryGetValue(featureType, out var cost) ? cost : 0;
    }

    public static bool IsFree(AIFeatureType featureType)
    {
        return GetCost(featureType) == 0;
    }
}
