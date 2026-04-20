using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperProjects.DeleteProject;

[Route("api/helper-projects")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class DeleteProjectEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProject(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteProjectRequest(id), ct);
        return result.ToResponse();
    }
}
