using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.Seeker.UpdateSeekerProfile;

[Route("api/profile/seeker")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class UpdateSeekerProfileEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut]
    public async Task<IActionResult> UpdateSeekerProfile([FromBody] UpdateSeekerProfileRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
