using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperServices.UpdateService;

[Route("api/helper-services")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class UpdateServiceEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceRequest request, CancellationToken ct)
    {
        if (id != request.Id)
            return BadRequest();

        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
