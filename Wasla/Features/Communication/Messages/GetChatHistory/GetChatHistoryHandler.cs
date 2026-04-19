namespace Wasla.Features.Communication.Messages.GetChatHistory;

public class GetChatHistoryHandler(
    IRepository<Message> messageRepo,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetChatHistoryRequest, Result<GetChatHistoryResponse>>
{
    private readonly IRepository<Message> _messageRepo = messageRepo;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result<GetChatHistoryResponse>> Handle(GetChatHistoryRequest request, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();

        var query = _messageRepo.GetAll()
            .Where(m =>
                ((m.SenderId == userId && m.ReceiverId == request.OtherUserId && !m.IsDeletedBySender) ||
                 (m.SenderId == request.OtherUserId && m.ReceiverId == userId && !m.IsDeletedByReceiver)))
            .OrderByDescending(m => m.CreatedOn);

        var paginatedList = await PaginatedList<ChatMessageDto>.CreateAsync(
            query.Select(m => new ChatMessageDto(
                m.Id, m.SenderId, m.ReceiverId, m.Content, m.Type, m.IsRead, m.CreatedOn
            )),
            request.PageNumber, request.PageSize, ct
        );

        return Result.Success(new GetChatHistoryResponse(
            paginatedList.Items,
            new PaginationResponse
            {
                PageNumber = paginatedList.PageNumber,
                PageSize = request.PageSize,
                TotalPages = paginatedList.TotalPages,
                HasNextPage = paginatedList.HasNextPage
            }
        ));
    }
}
