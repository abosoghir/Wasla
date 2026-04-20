using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.ProjectMilestones.SubmitDeliverable;

[Route("api/milestones")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class SubmitDeliverableEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("{id:int}/submit")]
    public async Task<IActionResult> SubmitDeliverable(int id, [FromBody] SubmitDeliverableRequest request, CancellationToken ct)
    {
        if (id != request.MilestoneId) return BadRequest();
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
