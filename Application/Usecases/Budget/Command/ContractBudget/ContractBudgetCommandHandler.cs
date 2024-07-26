using System.Net;
using Application.Usecases.OrderService.Command.ContractService;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.Budget.Command.ContractBudget;

public class ContractBudgetCommandHandler(IBudgetRepository budgetRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<ContractBudgetCommand, ContractBudgetResult>
{
    public async Task<ContractBudgetResult> Handle(ContractBudgetCommand request, CancellationToken cancellationToken)
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
                
                return new ContractBudgetResult();
            }
            
            var budgetEntity = budgetEntities.First();

            budgetEntity.Status = StatusBudgetConsts.GetStatusBudgetEnum(StatusBudgetEnum.Accepted);
            budgetEntity.DateAccepted = DateTime.Now;
            budgetEntity.DateChoose = request.DateServiceSelected;
            await budgetRepository.Update(budgetEntity, cancellationToken);

            await mediator.Send(new ContractServiceCommand()
            {
                OrderServiceId = budgetEntity.OrderServiceId
            }, cancellationToken);
            
            return new ContractBudgetResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();
                
            return new ContractBudgetResult();
        }
    }
}