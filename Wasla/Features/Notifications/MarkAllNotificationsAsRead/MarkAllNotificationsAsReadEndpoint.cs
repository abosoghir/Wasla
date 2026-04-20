using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Notifications.MarkAllNotificationsAsRead;

[Route("api/notifications")]
[ApiController]
[Authorize]
public class MarkAllNotificationsAsReadEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead(CancellationToken ct)
    {
        var result = await _mediator.Send(new MarkAllNotificationsAsReadRequest(), ct);
        return result.ToResponse();
    }
}
