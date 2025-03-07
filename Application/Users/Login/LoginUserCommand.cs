using Application.DTOs;
using MediatR;

namespace Application.Users.Login;

public record LoginUserCommand(LoginUserDto User) : IRequest<string>;
