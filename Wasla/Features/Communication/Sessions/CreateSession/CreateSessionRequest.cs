namespace Wasla.Features.Communication.Sessions.CreateSession;

public record CreateSessionRequest(int HelperId, DateTime ScheduledAt, int DurationMinutes, decimal TotalPrice, string? MeetingPlatform, string? Notes)
    : IRequest<Result<CreateSessionResponse>>;

public record CreateSessionResponse(int Id);

public class CreateSessionRequestValidator : AbstractValidator<CreateSessionRequest>
{
    public CreateSessionRequestValidator()
    {
        RuleFor(x => x.HelperId).GreaterThan(0);
        RuleFor(x => x.ScheduledAt).GreaterThan(DateTime.UtcNow);
        RuleFor(x => x.DurationMinutes).GreaterThan(0).LessThanOrEqualTo(480);
        RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0);
    }
}
