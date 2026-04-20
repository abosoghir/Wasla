using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.TaskOffers.CreateOffer;

[Route("api/task-offers")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class CreateOfferEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateOffer([FromBody] CreateOfferRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
