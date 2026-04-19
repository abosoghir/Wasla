using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Tasks.UpdateTask;

[Route("api/tasks")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class UpdateTaskEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskRequest request, CancellationToken ct)
    {
        if (id != request.Id) return BadRequest();
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
