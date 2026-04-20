using Microsoft.AspNetCore.Mvc;

namespace Wasla.Features.AuthenticationManagement.Login;

[Route("api/auth")]
[ApiController]
public class LoginEndPoint(IMediator mediator, ILogger<LoginEndPoint> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<LoginEndPoint> _logger = logger;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {


        _logger.LogInformation("Login with email: {email} and password: {password}", request.Email, request.Password);

        var result = await _mediator.Send(request, ct);

        return result.ToResponse();
    }
}
