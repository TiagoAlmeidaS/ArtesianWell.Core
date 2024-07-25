namespace Application.Usecases.OrderService.Command.SetServiceScheduleDate;

public class SetServiceScheduleDateResult
{
    public string Id { get; set; }
    public string ClientId { get; set; }
    public DateTime BudgetSchedulingDate { get; set; }
    public DateTime ServiceSchedulingDate { get; set; }
    public string Status { get; set; }
    public string ServiceId { get; set; }
}