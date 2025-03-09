using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserResponse>
{
    private readonly IUserRepository _repository;
    private readonly IAppDbContext _context;

    public GetUserQueryHandler(IUserRepository repository, IAppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<Result<UserResponse>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        UserResponse? user = await _context.Users
            .Where(u => u.Id == query.Id)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.Id));
        }

        return user;
    }
}
