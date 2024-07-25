using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Budget.Query.GetBudgetsByOrderServiceId;

public class GetBudgetsByOrderServiceIdQueryHandler(IBudgetRepository budgetRepository, IMessageHandlerService msg): IRequestHandler<GetBudgetsByOrderServiceIdQuery, List<GetBudgetsByOrderServiceIdResult>>
{
    public async Task<List<GetBudgetsByOrderServiceIdResult>> Handle(GetBudgetsByOrderServiceIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var budgetEntities = await budgetRepository.GetWhere(x => x.OrderServiceId == request.OrderServiceId, cancellationToken);
            if (!budgetEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorOrderServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new List<GetBudgetsByOrderServiceIdResult>();
            }
            
            return budgetEntities.Select(budgetEntity => new GetBudgetsByOrderServiceIdResult()
            {
                Id = budgetEntity.Id,
                Status = budgetEntity.Status,
                DateAccepted = budgetEntity.DateAccepted,
                DateChoose = budgetEntity.DateChoose,
                DescriptionService = budgetEntity.DescriptionService,
                TotalValue = budgetEntity.TotalValue,
                OrderServiceId = budgetEntity.OrderServiceId
            }).ToList();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();
                
            return new List<GetBudgetsByOrderServiceIdResult>();
        }
    }
}