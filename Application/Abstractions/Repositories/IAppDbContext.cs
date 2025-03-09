using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Repositories;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
}
