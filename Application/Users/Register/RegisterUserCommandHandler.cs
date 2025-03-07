using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<User>>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _repository;

    public RegisterUserCommandHandler(IPasswordHasher passwordHasher, IUserRepository repository)
    {
        _passwordHasher = passwordHasher;
        _repository = repository;
    }

    public async Task<Result<User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.IsEmailUnique(request.User.Email))
            return Result<User>.Failure("Email already exists.");

        var user = new User
        {
            FirstName = request.User.FirstName,
            LastName = request.User.LastName,
            Email = request.User.Email,
            PasswordHash = _passwordHasher.Hash(request.User.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(user);

        return Result<User>.Success(user);
    }
}
