using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.User.UpdateUserProfile;

[Route("api/profile")]
[ApiController]
[Authorize]
public class UpdateUserProfileEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("me")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
