using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.Helper.GetHelperEarnings;

[Route("api/profile/helper")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class GetHelperEarningsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("earnings")]
    public async Task<IActionResult> GetHelperEarnings([FromQuery] GetHelperEarningsRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
