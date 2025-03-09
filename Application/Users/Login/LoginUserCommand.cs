using Application.Abstractions.Messaging;
using Application.DTOs;

namespace Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
