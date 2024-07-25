namespace Application.Usecases.OrderService.Query.GetOrderServiceByClient;

public class GetOrderServiceByClientResult
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public DateTime BudgetSchedulingDate { get; set; }
    public DateTime ServiceSchedulingDate { get; set; }
    public string Status { get; set; }
    public string ServiceId { get; set; }
}