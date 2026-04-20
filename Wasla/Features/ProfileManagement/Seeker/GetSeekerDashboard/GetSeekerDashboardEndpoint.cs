using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.Seeker.GetSeekerDashboard;

[Route("api/profile/seeker")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class GetSeekerDashboardEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetSeekerDashboard(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetSeekerDashboardRequest(), ct);
        return result.ToResponse();
    }
}
