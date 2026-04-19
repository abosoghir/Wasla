namespace Wasla.Features.Communication.Sessions.CompleteSession;

public record CompleteSessionRequest(int Id) : IRequest<Result>;
