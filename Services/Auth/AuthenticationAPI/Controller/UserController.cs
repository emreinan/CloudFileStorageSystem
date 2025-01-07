using AuthenticationAPI.Application.Features.Users.Queries.GetList;
using AuthenticationAPI.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await mediator.Send(new GetUsersListQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await mediator.Send(new GetUserByIdQuery { Id = id });
        return Ok(user);
    }

}
