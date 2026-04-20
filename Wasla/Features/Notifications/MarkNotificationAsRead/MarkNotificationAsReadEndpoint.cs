using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Notifications.MarkNotificationAsRead;

[Route("api/notifications")]
[ApiController]
[Authorize]
public class MarkNotificationAsReadEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/read")]
    public async Task<IActionResult> MarkAsRead(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new MarkNotificationAsReadRequest(id), ct);
        return result.ToResponse();
    }
}
