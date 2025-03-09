using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Domain;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _repository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserCommandHandler(IUserRepository repository,
        ITokenProvider tokenProvider,
        IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmail(command.Email);

        if (user is null)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        bool verified = _passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        string token = _tokenProvider.Create(user);

        return token;
    }
}
