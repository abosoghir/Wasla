using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Financial.Wallet.GetWallet;

[Route("api/wallet")]
[ApiController]
[Authorize]
public class GetWalletEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetWallet(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetWalletRequest(), ct);
        return result.ToResponse();
    }
}
