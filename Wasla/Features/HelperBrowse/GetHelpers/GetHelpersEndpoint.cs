using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperBrowse.GetHelpers;

[Route("api/helpers")]
[ApiController]
public class GetHelpersEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetHelpers([FromQuery] GetHelpersRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
