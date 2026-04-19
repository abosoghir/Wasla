namespace Wasla.Features.Communication.Messages.GetChatHistory;

public record GetChatHistoryRequest(string OtherUserId, int PageNumber = 1, int PageSize = 50)
    : IRequest<Result<GetChatHistoryResponse>>;

public record GetChatHistoryResponse(List<ChatMessageDto> Items, PaginationResponse Pagination);

public record ChatMessageDto(int Id, string SenderId, string ReceiverId, string Content, MessageType Type,
    bool IsRead, DateTime CreatedOn);
