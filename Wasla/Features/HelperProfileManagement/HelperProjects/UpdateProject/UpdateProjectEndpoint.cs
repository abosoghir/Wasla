using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperProjects.UpdateProject;

[Route("api/helper-projects")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class UpdateProjectEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectRequest request, CancellationToken ct)
    {
        if (id != request.Id)
            return BadRequest();

        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
