using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Financial.Wallet.WithdrawFunds;

[Route("api/wallet")]
[ApiController]
[Authorize]
public class WithdrawFundsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawFunds([FromBody] WithdrawFundsRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
