using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperProjects.CreateProject;

[Route("api/helper-projects")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class CreateProjectEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateHelperProjectRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
