namespace Application.Usecases.Budget.Command.CreateBudget;

public class CreateBudgetResult
{
    public string Id { get; set; }
    public string OrderServiceId { get; set; }
    public DateTime DateAccepted { get; set; }
    public decimal TotalValue { get; set; }
    public string DescriptionService { get; set; }
    public String Status { get; set; }
    public DateTime DateChoose { get; set; }
}