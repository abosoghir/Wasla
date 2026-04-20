using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Projects.ApplyToProject;

[Route("api/projects")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class ApplyToProjectEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("{id:int}/apply")]
    public async Task<IActionResult> ApplyToProject(int id, [FromBody] ApplyToProjectRequest request, CancellationToken ct)
    {
        var actualRequest = request with { ProjectId = id };
        var result = await _mediator.Send(actualRequest, ct);
        return result.ToResponse();
    }
}
