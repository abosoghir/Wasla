namespace Wasla.Features.Communication.Messages.SendMessage;

public record SendMessageRequest(string ReceiverId, string Content, MessageType Type, int? TaskId, int? ProjectId, int? SessionId)
    : IRequest<Result<SendMessageResponse>>;

public record SendMessageResponse(int Id);

public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x.ReceiverId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
        RuleFor(x => x.Type).IsInEnum();
    }
}
