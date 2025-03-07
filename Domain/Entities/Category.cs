using Domain.Enums;

namespace Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}
