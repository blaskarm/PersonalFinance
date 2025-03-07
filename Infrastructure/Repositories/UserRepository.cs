using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);

        return user!;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _context.Users.AnyAsync(u => u.Email == email);
    }
}
