using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AuthenticationManagement.RefreshToken;

[Route("auth")]
[ApiController]
public class RefreshTokenEndPoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.ToResponse();
    }
}
