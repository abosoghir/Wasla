using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperBrowse.GetHelperById;

[Route("api/helpers")]
[ApiController]
public class GetHelperByIdEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetHelperById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetHelperByIdRequest(id), ct);
        return result.ToResponse();
    }
}
