using Application.DTOs;
using Application.Users.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserControllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUser)
    {
        try
        {
            string result = await _mediator.Send(new LoginUserCommand(loginUser));

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
