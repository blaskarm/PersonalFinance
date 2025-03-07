using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Users.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _repository;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserCommandHandler(IUserRepository repository, ITokenProvider tokenProvider, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmail(request.User.Email);

        if (user is null)
            return "Wrong Email.";

        bool verified = _passwordHasher.Verify(request.User.Password, user.PasswordHash);

        if (!verified)
            return "Wrong Password.";

        string token = _tokenProvider.Create(user);

        return token;
    }
}
