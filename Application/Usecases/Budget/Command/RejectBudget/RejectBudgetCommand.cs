using MediatR;

namespace Application.Usecases.Budget.Command.RejectBudget;

public class RejectBudgetCommand : IRequest<RejectBudgetResult>
{
    public string BudgetId { get; set; }
}