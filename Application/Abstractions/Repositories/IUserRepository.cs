using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsEmailUnique(string email);
    Task<User> GetByEmail(string email);
}
