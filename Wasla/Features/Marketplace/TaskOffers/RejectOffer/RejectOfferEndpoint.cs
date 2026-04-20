using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.TaskOffers.RejectOffer;

[Route("api/task-offers")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class RejectOfferEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/reject")]
    public async Task<IActionResult> RejectOffer(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new RejectOfferRequest(id), ct);
        return result.ToResponse();
    }
}
