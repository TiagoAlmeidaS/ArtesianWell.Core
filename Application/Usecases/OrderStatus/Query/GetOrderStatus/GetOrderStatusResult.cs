namespace Application.Usecases.OrderStatus.Query.GetOrderStatus;

public class GetOrderStatusResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PossibleActions { get; set; }
}