namespace Domain.Entities;

public class Income
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public Guid? CategoryId { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public required string Source { get; set; }
}
