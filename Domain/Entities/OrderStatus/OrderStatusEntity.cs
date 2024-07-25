using Domain.SeedWork;

namespace Domain.Entities.OrderStatus;

public class OrderStatusEntity: AggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PossibleActions { get; set; }
}