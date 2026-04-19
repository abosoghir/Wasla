namespace Wasla.Features.Communication;

public static class CommunicationErrors
{
    public static readonly Error MessageNotFound =
        new("Communication.MessageNotFound", "Message not found.", StatusCodes.Status404NotFound);

    public static readonly Error SessionNotFound =
        new("Communication.SessionNotFound", "Session not found.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidSessionStatus =
        new("Communication.InvalidSessionStatus", "Session is not in the correct status for this action.", StatusCodes.Status400BadRequest);

    public static readonly Error Unauthorized =
        new("Communication.Unauthorized", "You are not authorized to perform this action.", StatusCodes.Status403Forbidden);

    public static readonly Error SeekerNotFound =
        new("Communication.SeekerNotFound", "Seeker profile not found.", StatusCodes.Status404NotFound);

    public static readonly Error HelperNotFound =
        new("Communication.HelperNotFound", "Helper profile not found.", StatusCodes.Status404NotFound);

    public static readonly Error CannotMessageSelf =
        new("Communication.CannotMessageSelf", "You cannot send a message to yourself.", StatusCodes.Status400BadRequest);
}
