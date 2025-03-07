using Application.Common;
using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Users.Register;

public sealed record RegisterUserCommand(UserDto User) : IRequest<Result<User>>;
