namespace Application.Usecases.Budget.Query.GetBudgetsWithPendentsStatus;

public class GetBudgetsWithPendentsStatusResult
{
    public string Id { get; set; }
    public string OrderServiceId { get; set; }
    public DateTime DateAccepted { get; set; }
    public decimal TotalValue { get; set; }
    public string DescriptionService { get; set; }
    public String Status { get; set; }
    public DateTime DateChoose { get; set; }
}