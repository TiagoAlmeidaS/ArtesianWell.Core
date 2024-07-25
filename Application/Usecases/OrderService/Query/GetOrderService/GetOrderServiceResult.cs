namespace Application.Usecases.OrderService.Query.GetOrderService;

public class GetOrderServiceResult
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public DateTime BudgetSchedulingDate { get; set; }
    public DateTime ServiceSchedulingDate { get; set; }
    public string Status { get; set; }
    public string ServiceId { get; set; }
}