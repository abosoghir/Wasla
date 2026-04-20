using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.Helper.GetHelperPoints;

[Route("api/profile/helper")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class GetHelperPointsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("points")]
    public async Task<IActionResult> GetHelperPoints(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetHelperPointsRequest(), ct);
        return result.ToResponse();
    }
}
