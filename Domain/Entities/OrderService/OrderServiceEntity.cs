using Domain.SeedWork;

namespace Domain.Entities.OrderService;

public class OrderServiceEntity: AggregateRoot
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public DateTime BudgetSchedulingDate { get; set; }
    public DateTime ServiceSchedulingDate { get; set; } = DateTime.MinValue;
    public string Status { get; set; }
    public string ServiceId { get; set; }
}