using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Financial.Payments.ConfirmPayment;

[Route("api/payments")]
[ApiController]
[Authorize]
public class ConfirmPaymentEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/confirm")]
    public async Task<IActionResult> ConfirmPayment(int id, [FromBody] ConfirmPaymentRequest request, CancellationToken ct)
    {
        if (id != request.Id) return BadRequest();
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
