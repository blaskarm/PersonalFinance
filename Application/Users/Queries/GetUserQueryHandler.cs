using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<User>>
{
    private readonly IUserRepository _repository;

    public GetUserQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id);

        return Result<User>.Success(user);
    }
}
