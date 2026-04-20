using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.ProjectMilestones.CreateMilestone;

[Route("api/milestones")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class CreateMilestoneEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateMilestone([FromBody] CreateMilestoneRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
