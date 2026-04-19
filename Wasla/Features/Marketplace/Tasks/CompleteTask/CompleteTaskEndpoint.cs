using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Tasks.CompleteTask;

[Route("api/tasks")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class CompleteTaskEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/complete")]
    public async Task<IActionResult> CompleteTask(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new CompleteTaskRequest(id), ct);
        return result.ToResponse();
    }
}
