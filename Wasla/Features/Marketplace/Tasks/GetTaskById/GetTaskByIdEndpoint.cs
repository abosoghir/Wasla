using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Marketplace.Tasks.GetTaskById;

[Route("api/tasks")]
[ApiController]
public class GetTaskByIdEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTaskById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetTaskByIdRequest(id), ct);
        return result.ToResponse();
    }
}
