using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Financial.Wallet.MockDepositFunds;

[Route("api/wallet")]
[ApiController]
[Authorize]
public class MockDepositFundsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositFunds([FromBody] MockDepositFundsRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
