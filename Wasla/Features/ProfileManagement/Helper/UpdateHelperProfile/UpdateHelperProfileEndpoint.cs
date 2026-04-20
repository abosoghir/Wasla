using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.Helper.UpdateHelperProfile;

[Route("api/profile/helper")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class UpdateHelperProfileEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut]
    public async Task<IActionResult> UpdateHelperProfile([FromBody] UpdateHelperProfileRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
