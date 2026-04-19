using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.HelperProfileManagement.HelperServices.DeleteService;

[Route("api/helper-services")]
[ApiController]
[Authorize(Roles = DefaultRoles.Helper)]
public class DeleteServiceEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteService(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new DeleteServiceRequest(id), ct);
        return result.ToResponse();
    }
}
