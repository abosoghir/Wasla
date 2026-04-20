using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.TaskOffers.AcceptOffer;

[Route("api/task-offers")]
[ApiController]
[Authorize(Roles = DefaultRoles.Seeker)]
public class AcceptOfferEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}/accept")]
    public async Task<IActionResult> AcceptOffer(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new AcceptOfferRequest(id), ct);
        return result.ToResponse();
    }
}
