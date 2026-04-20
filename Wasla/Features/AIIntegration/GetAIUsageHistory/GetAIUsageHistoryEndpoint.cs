using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AIIntegration.GetAIUsageHistory;

[Route("api/ai")]
[ApiController]
[Authorize]
public class GetAIUsageHistoryEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("usage")]
    public async Task<IActionResult> GetAIUsageHistory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetAIUsageHistoryRequest(pageNumber, pageSize), ct);
        return result.ToResponse();
    }
}
