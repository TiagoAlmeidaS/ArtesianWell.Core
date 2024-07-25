using Domain.SeedWork;

namespace Domain.Entities.Budget;

public class BudgetEntity: AggregateRoot
{
    public string Id { get; set; }
    public string OrderServiceId { get; set; }
    public DateTime DateAccepted { get; set; } = DateTime.MinValue;
    public decimal TotalValue { get; set; }
    public string DescriptionService { get; set; }
    public string Status { get; set; }
    public DateTime DateChoose { get; set; } = DateTime.MinValue;
}