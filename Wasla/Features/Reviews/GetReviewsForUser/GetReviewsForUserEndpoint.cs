using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.Reviews.GetReviewsForUser;

[Route("api/reviews")]
[ApiController]
public class GetReviewsForUserEndpoint(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReviewsForUser(string userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetReviewsForUserRequest(userId, pageNumber, pageSize), ct);
        return result.ToResponse();
    }
}
