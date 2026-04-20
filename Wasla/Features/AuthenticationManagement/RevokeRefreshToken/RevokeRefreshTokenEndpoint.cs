using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AuthenticationManagement.RevokeRefreshToken;

[Route("api/auth")]
[ApiController]
public class RevokeRefreshTokenEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.ToResponse();
    }
}
