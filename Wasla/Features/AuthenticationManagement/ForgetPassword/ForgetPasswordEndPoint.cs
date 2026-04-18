using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AuthenticationManagement.ForgetPassword;

[Route("auth")]
[ApiController]
public class ForgetPasswordEndPoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.ToResponse();
    }
}
