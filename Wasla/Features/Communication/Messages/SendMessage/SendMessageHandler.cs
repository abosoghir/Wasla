namespace Wasla.Features.Communication.Messages.SendMessage;

public class SendMessageHandler(
    IRepository<Message> messageRepo,
    IRepository<Notification> notificationRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<SendMessageRequest, Result<SendMessageResponse>>
{
    private readonly IRepository<Message> _messageRepo = messageRepo;
    private readonly IRepository<Notification> _notificationRepo = notificationRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<SendMessageResponse>> Handle(SendMessageRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        if (userId == request.ReceiverId)
            return Result.Failure<SendMessageResponse>(CommunicationErrors.CannotMessageSelf);

        var message = new Message
        {
            SenderId = userId!,
            ReceiverId = request.ReceiverId,
            Content = request.Content,
            Type = request.Type,
            TaskId = request.TaskId,
            ProjectId = request.ProjectId,
            SessionId = request.SessionId,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _messageRepo.AddAsync(message, ct);

        var notification = new Notification
        {
            UserId = request.ReceiverId,
            Type = NotificationType.MessageReceived,
            Title = "New Message",
            Body = request.Content.Length > 100 ? request.Content[..100] + "..." : request.Content,
            RelatedEntityType = "Message",
            RelatedEntityId = message.Id,
            CreatedById = userId!,
            CreatedOn = DateTime.UtcNow
        };

        await _notificationRepo.AddAsync(notification, ct);
        await _messageRepo.SaveChangesAsync(ct);

        return Result.Success(new SendMessageResponse(message.Id));
    }
}
