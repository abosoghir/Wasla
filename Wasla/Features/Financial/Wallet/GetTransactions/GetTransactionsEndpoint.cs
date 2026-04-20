using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Financial.Wallet.GetTransactions;

[Route("api/wallet")]
[ApiController]
[Authorize]
public class GetTransactionsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetTransactionsRequest(pageNumber, pageSize), ct);
        return result.ToResponse();
    }
}
