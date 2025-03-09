using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserRole UserRole { get; set; }
    public List<Budget> Budgets { get; set; } = null!;
    public List<Category> Categories { get; set; } = null!;
}
