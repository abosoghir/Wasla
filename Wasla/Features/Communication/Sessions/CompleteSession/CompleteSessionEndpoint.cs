using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Communication.Sessions.CompleteSession;

[Route("api/sessions")]
[ApiController]
[Authorize]
public class CompleteSessionEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/complete")]
    public async Task<IActionResult> CompleteSession(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new CompleteSessionRequest(id), ct);
        return result.ToResponse();
    }
}
