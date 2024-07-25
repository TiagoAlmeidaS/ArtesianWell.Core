namespace Application.Usecases.OrderStatus.Query.GetAllOrderStatus;

public class GetAllOrderStatusResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string PossibleActions { get; set; }
}