using API.Extensions;
using API.Infrastructure;
using Application.DTOs;
using Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserControllers;

[Route("api/[controller]")]
[ApiController]
public sealed class RegisterController : ControllerBase
{
    public sealed record Request(string Email, string FirstName, string LastName, string Password);

    private readonly ISender _sender;

    public RegisterController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IResult> Register([FromBody] Request request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

        var result = await _sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);

        //if (!result.IsSuccess)
        //    return BadRequest(result.Message);

        //return Ok(result.Message);
    }
}
