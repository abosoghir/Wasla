using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.TaskOffers.GetMyOffers;

[Route("api/task-offers")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class GetMyOffersEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("my")]
    public async Task<IActionResult> GetMyOffers([FromQuery] GetMyOffersRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
