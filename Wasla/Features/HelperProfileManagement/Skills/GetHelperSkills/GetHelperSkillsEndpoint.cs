using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.Skills.GetHelperSkills;

[Route("api/helper-skills")]
[ApiController]
public class GetHelperSkillsEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("helper/{helperId:int}")]
    public async Task<IActionResult> GetHelperSkills(int helperId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetHelperSkillsRequest(helperId), ct);
        return result.ToResponse();
    }
}
