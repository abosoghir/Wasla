using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Tasks.DeleteTask;

[Route("api/tasks")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class DeleteTaskEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteTaskRequest(id), ct);
        return result.ToResponse();
    }
}
