using MediatR;

namespace Application.Usecases.Budget.Query.GetBudgetsByOrderServiceId;

public class GetBudgetsByOrderServiceIdQuery : IRequest<List<GetBudgetsByOrderServiceIdResult>>
{
    public string OrderServiceId { get; set; }
}