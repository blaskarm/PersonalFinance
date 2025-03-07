using Application.DTOs;
using Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserControllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDto user)
    {
        try
        {
            var result = await _mediator.Send(new RegisterUserCommand(user));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
