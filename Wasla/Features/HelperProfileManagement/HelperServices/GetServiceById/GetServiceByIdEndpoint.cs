using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperServices.GetServiceById;

[Route("api/helper-services")]
[ApiController]
public class GetServiceByIdEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetServiceById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetServiceByIdRequest(id), ct);
        return result.ToResponse();
    }
}
