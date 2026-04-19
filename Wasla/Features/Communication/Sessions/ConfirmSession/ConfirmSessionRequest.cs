namespace Wasla.Features.Communication.Sessions.ConfirmSession;

public record ConfirmSessionRequest(int Id, string? MeetingLink) : IRequest<Result>;
