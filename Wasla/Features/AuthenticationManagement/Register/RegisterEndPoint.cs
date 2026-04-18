using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AuthenticationManagement.Register;

[Route("auth")]
[ApiController]
public class RegisterEndPoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.ToResponse();
    }
}
