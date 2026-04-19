using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Communication.Messages.MarkAsRead;

[Route("api/messages")]
[ApiController]
[Authorize]
public class MarkAsReadEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("read")]
    public async Task<IActionResult> MarkAsRead([FromBody] MarkAsReadRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
