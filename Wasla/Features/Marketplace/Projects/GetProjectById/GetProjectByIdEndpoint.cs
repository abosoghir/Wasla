using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Projects.GetProjectById;

[Route("api/projects")]
[ApiController]
public class GetProjectByIdEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProjectById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProjectByIdRequest(id), ct);
        return result.ToResponse();
    }
}
