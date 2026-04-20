using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Projects.GetProjects;

[Route("api/projects")]
[ApiController]
public class GetProjectsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetProjects([FromQuery] GetProjectsRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
