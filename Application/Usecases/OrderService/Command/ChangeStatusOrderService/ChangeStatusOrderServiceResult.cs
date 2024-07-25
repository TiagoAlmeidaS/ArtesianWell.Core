namespace Application.Usecases.OrderService.Command.ChangeStatusOrderService;

public class ChangeStatusOrderServiceResult
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public DateTime BudgetSchedulingDate { get; set; }
    public DateTime ServiceSchedulingDate { get; set; }
    public string Status { get; set; }
    public string ServiceId { get; set; }
}