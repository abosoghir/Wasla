namespace Wasla.Features.HelperProfileManagement;

public static class HelperProfileErrors
{
    public static readonly Error HelperNotFound =
        new("HelperProfile.HelperNotFound", "Helper profile not found.", StatusCodes.Status404NotFound);

    public static readonly Error ServiceNotFound =
        new("HelperProfile.ServiceNotFound", "Service not found.", StatusCodes.Status404NotFound);

    public static readonly Error ProjectNotFound =
        new("HelperProfile.ProjectNotFound", "Portfolio project not found.", StatusCodes.Status404NotFound);

    public static readonly Error SkillNotFound =
        new("HelperProfile.SkillNotFound", "Skill not found.", StatusCodes.Status404NotFound);

    public static readonly Error SkillAlreadyAdded =
        new("HelperProfile.SkillAlreadyAdded", "This skill is already added to the helper's profile.", StatusCodes.Status409Conflict);

    public static readonly Error Unauthorized =
        new("HelperProfile.Unauthorized", "You are not authorized to perform this action.", StatusCodes.Status403Forbidden);

    public static readonly Error HelperSkillNotFound =
        new("HelperProfile.HelperSkillNotFound", "Helper skill not found.", StatusCodes.Status404NotFound);
}
