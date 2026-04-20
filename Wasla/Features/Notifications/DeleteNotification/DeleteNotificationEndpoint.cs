using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Notifications.DeleteNotification;

[Route("api/notifications")]
[ApiController]
[Authorize]
public class DeleteNotificationEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteNotification(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteNotificationRequest(id), ct);
        return result.ToResponse();
    }
}
