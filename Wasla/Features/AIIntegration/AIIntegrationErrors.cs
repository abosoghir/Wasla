namespace Wasla.Features.AIIntegration;

public static class AIIntegrationErrors
{
    public static readonly Error InsufficientPoints =
        new("AI.InsufficientPoints", "You do not have enough points to use this AI feature.", StatusCodes.Status400BadRequest);

    public static readonly Error HelperNotFound =
        new("AI.HelperNotFound", "Helper profile not found. Only helpers can use AI features.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidFeatureType =
        new("AI.InvalidFeatureType", "Invalid AI feature type.", StatusCodes.Status400BadRequest);
}
