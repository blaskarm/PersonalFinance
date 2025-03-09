using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Domain;
using Domain.Entities;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _repository;

    public RegisterUserCommandHandler(IPasswordHasher passwordHasher, IUserRepository repository)
    {
        _passwordHasher = passwordHasher;
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (!await _repository.IsEmailUnique(command.Email))
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            PasswordHash = _passwordHasher.Hash(command.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(user);

        return user.Id;
    }
}
