using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.ProjectMilestones.ApproveMilestone;

[Route("api/milestones")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class ApproveMilestoneEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/approve")]
    public async Task<IActionResult> ApproveMilestone(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new ApproveMilestoneRequest(id), ct);
        return result.ToResponse();
    }
}
