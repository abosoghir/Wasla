using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.ProfileManagement.User.GetMyProfile;

[Route("api/profile")]
[ApiController]
[Authorize]
public class GetMyProfileEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetMyProfileRequest(), ct);
        return result.ToResponse();
    }
}
