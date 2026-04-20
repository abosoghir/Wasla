using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperProjects.GetHelperProjects;

[Route("api/helper-projects")]
[ApiController]
public class GetHelperProjectsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("helper/{helperId:int}")]
    public async Task<IActionResult> GetHelperProjects(int helperId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetHelperProjectsRequest(helperId), ct);
        return result.ToResponse();
    }
}
