namespace Wasla.Features.Communication.Messages.MarkAsRead;

public class MarkAsReadHandler(
    IRepository<Message> messageRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<MarkAsReadRequest, Result>
{
    private readonly IRepository<Message> _messageRepo = messageRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> Handle(MarkAsReadRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        // Only the receiver can mark messages as read
        await _messageRepo.BulkUpdateWhereAsync(
            m => request.MessageIds.Contains(m.Id) && m.ReceiverId == userId && !m.IsRead,
            s => s.SetProperty(m => m.IsRead, true)
                  .SetProperty(m => m.ReadAt, DateTime.UtcNow),
            ct);

        return Result.Success();
    }
}
