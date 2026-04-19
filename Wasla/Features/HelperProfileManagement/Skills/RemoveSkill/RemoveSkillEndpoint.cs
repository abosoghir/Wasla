using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.Skills.RemoveSkill;

[Route("api/helper-skills")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class RemoveSkillEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveSkill(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new RemoveSkillRequest(id), ct);
        return result.ToResponse();
    }
}
