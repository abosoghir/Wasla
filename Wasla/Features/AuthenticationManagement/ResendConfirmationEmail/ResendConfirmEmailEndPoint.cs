using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AuthenticationManagement.ResendConfirmationEmail;

[Route("api/auth")]
[ApiController]
public class ResendConfirmEmailEndPoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmEmailRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.ToResponse();
    }
}
