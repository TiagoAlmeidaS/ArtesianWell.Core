using MediatR;

namespace Application.Usecases.Budget.Command.ContractBudget;

public class ContractBudgetCommand : IRequest<ContractBudgetResult>
{
    public string BudgetId { get; set; }
    public DateTime DateServiceSelected { get; set; }
}