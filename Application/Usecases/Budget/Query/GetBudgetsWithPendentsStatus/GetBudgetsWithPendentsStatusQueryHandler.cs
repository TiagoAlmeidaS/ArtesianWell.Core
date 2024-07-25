using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.Budget.Query.GetBudgetsWithPendentsStatus;

public class GetBudgetsWithPendentsStatusQueryHandler(IBudgetRepository budgetRepository, IMessageHandlerService msg): IRequestHandler<GetBudgetsWithPendentsStatusQuery, List<GetBudgetsWithPendentsStatusResult>>
{
    public async Task<List<GetBudgetsWithPendentsStatusResult>> Handle(GetBudgetsWithPendentsStatusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            
            var budgetEntities = await budgetRepository.GetWhere(x => x.Status == StatusBudgetConsts.GetStatusBudgetEnum(StatusBudgetEnum.WatingForAction), cancellationToken);
            if (!budgetEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorOrderServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new List<GetBudgetsWithPendentsStatusResult>();
            }
            
            return budgetEntities.Select(budgetEntity => new GetBudgetsWithPendentsStatusResult()
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
                
            return new List<GetBudgetsWithPendentsStatusResult>();
        }
    }
}