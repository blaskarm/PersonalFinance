namespace Domain.Entities;

public class Budget
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public double TotalAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Expense> Expenses { get; set; } = null!;
    public List<Income> Incomes { get; set; } = null!;
}
