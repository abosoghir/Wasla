using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperServices.GetHelperServices;

[Route("api/helper-services")]
[ApiController]
public class GetHelperServicesEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("helper/{helperId:int}")]
    public async Task<IActionResult> GetHelperServices(int helperId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetHelperServicesRequest(helperId), ct);
        return result.ToResponse();
    }
}
