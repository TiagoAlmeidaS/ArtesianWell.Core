namespace Application.Usecases.OrderService.Command.CreateOrderService;

public class CreateOrderServiceResult
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public DateTime BudgetSchedulingDate { get; set; }
    public DateTime ServiceSchedulingDate { get; set; } = DateTime.MinValue;
    public string Status { get; set; }
    public string ServiceId { get; set; }
}