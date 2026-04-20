using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AIIntegration.UseAIFeature;

[Route("api/ai")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class UseAIFeatureEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("use")]
    public async Task<IActionResult> UseAIFeature([FromBody] UseAIFeatureRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
