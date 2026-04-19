using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.Skills.AddSkillToHelper;

[Route("api/helper-skills")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class AddSkillToHelperEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> AddSkillToHelper([FromBody] AddSkillToHelperRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
