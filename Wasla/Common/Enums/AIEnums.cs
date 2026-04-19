namespace Wasla.Common.Enums;

public enum AIFeatureType
{
    ChatAssistant = 1,           // Free
    CVImprovement = 2,           // 20 points
    FileSummarization = 3,       // 10 points
    ProfileEnhancement = 4,      // 15 points
    ProposalGeneration = 5       // 25 points
}

public enum AIRequestStatus
{
    Pending = 0,
    Processing = 1,
    Success = 2,
    Failed = 3,
    Cancelled = 4,
    InsufficientPoints = 5
}
