namespace Wasla.Features.ProfileManagement;

public static class ProfileErrors
{
    public static readonly Error UserNotFound =
        new("Profile.UserNotFound", "User not found.", StatusCodes.Status404NotFound);

    public static readonly Error HelperProfileNotFound =
        new("Profile.HelperProfileNotFound", "Helper profile not found.", StatusCodes.Status404NotFound);

    public static readonly Error SeekerProfileNotFound =
        new("Profile.SeekerProfileNotFound", "Seeker profile not found.", StatusCodes.Status404NotFound);
}
