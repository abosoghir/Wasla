using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Communication.Messages.GetChatHistory;

[Route("api/messages")]
[ApiController]
[Authorize]
public class GetChatHistoryEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("chat/{otherUserId}")]
    public async Task<IActionResult> GetChatHistory(string otherUserId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetChatHistoryRequest(otherUserId, pageNumber, pageSize), ct);
        return result.ToResponse();
    }
}
