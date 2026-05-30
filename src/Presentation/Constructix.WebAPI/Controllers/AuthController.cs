using Constructix.Application.Features.AppUsers.Login;
using Constructix.Application.Features.AppUsers.Register;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Constructix.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        if (!response.Succeeded) return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        if (!response.IsAuthenticated) return Unauthorized(response);
        return Ok(response);
    }
}
