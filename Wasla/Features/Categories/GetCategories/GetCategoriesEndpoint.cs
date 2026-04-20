using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Categories.GetCategories;

[Route("api/categories")]
[ApiController]
public class GetCategoriesEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCategoriesRequest(), ct);
        return result.ToResponse();
    }
}
