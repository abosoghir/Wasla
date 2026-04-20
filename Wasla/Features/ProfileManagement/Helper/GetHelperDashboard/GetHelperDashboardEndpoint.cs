using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.Helper.GetHelperDashboard;

[Route("api/profile/helper")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class GetHelperDashboardEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetHelperDashboard(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetHelperDashboardRequest(), ct);
        return result.ToResponse();
    }
}
