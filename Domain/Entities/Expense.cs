namespace Domain.Entities;

public class Expense
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public Guid? CategoryId { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}
