using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Communication.Sessions.ConfirmSession;

[Route("api/sessions")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class ConfirmSessionEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/confirm")]
    public async Task<IActionResult> ConfirmSession(int id, [FromBody] ConfirmSessionRequest request, CancellationToken ct)
    {
        if (id != request.Id) return BadRequest();
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
