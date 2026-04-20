namespace Wasla.Features.HelperBrowse;

public static class HelperBrowseErrors
{
    public static readonly Error HelperNotFound =
        new("HelperBrowse.NotFound", "Helper not found.", StatusCodes.Status404NotFound);
}
