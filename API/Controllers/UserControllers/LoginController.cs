using API.Extensions;
using API.Infrastructure;
using Application.Users.Login;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserControllers;

[Route("api/[controller]")]
[ApiController]
public sealed class LoginController : ControllerBase
{
    public sealed record LoginRequest(string Email, string Password);

    private readonly ISender _sender;

    public LoginController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        Result<string> result = await _sender.Send(new LoginUserCommand(request.Email, request.Password), cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result.Value);
    }
}
