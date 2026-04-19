using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Tasks.GetTasks;

[Route("api/tasks")]
[ApiController]
public class GetTasksEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] GetTasksRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);
        return result.ToResponse();
    }
}
