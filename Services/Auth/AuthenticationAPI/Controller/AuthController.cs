using AuthenticationAPI.Application.Features.Auth.Commands.Login;
using AuthenticationAPI.Application.Features.Auth.Commands.RefreshToken;
using AuthenticationAPI.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
    {
        var command = new RegisterCommand { Register = registerDto };
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        var command = new LoginCommand { Login = loginDto };
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        var command = new RefreshTokenCommand { Token = refreshToken };
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
