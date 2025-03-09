using API.Extensions;
using API.Infrastructure;
using Application.Users.Queries;
using Domain;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace API.Controllers.UserControllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISender _sender;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger, ISender sender)
    {
        _mediator = mediator;
        _logger = logger;
        _sender = sender;
    }


    //[Authorize]
    [HasPermission(Permission.ReadMember)]
    [HttpGet]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery(id);

        Result<UserResponse> response = await _sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
