using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.Budget.Command.RejectBudget;

public class RejectBudgetCommandHandler(IBudgetRepository budgetRepository, IMessageHandlerService msg): IRequestHandler<RejectBudgetCommand, RejectBudgetResult>
{
    public async Task<RejectBudgetResult> Handle(RejectBudgetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var budgetEntities = await budgetRepository.GetWhere(x => x.Id == request.BudgetId, cancellationToken);
            if (!budgetEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorBudgetNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new RejectBudgetResult();
            }
            
            var budgetEntity = budgetEntities.First();
            
            budgetEntity.Status = StatusBudgetConsts.GetStatusBudgetEnum(StatusBudgetEnum.Rejected);
            budgetEntity.UpdatedAt = DateTime.Now;
            await budgetRepository.Update(budgetEntity, cancellationToken);
            
            return new RejectBudgetResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();
                
            return new RejectBudgetResult();
        }
    }
}