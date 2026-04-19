namespace Wasla.Features.Communication.Messages.MarkAsRead;

public record MarkAsReadRequest(List<int> MessageIds) : IRequest<Result>;

public class MarkAsReadRequestValidator : AbstractValidator<MarkAsReadRequest>
{
    public MarkAsReadRequestValidator()
    {
        RuleFor(x => x.MessageIds).NotEmpty();
    }
}
