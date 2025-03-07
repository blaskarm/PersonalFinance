using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
}
