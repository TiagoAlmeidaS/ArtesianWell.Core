using System.Net;
using Application.Usecases.OrderService.Command.SetBudgetOrderServiceStatus;
using Domain.Entities.Budget;
using Domain.Repositories;
using MediatR;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.Budget.Command.CreateBudget;

public class CreateBudgetCommandHandler(IBudgetRepository budgetRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<CreateBudgetCommand, CreateBudgetResult>
{
    public async Task<CreateBudgetResult> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await budgetRepository.Insert(new BudgetEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Status = StatusBudgetConsts.GetStatusBudgetEnum(StatusBudgetEnum.WatingForAction),
                DateAccepted = request.DateAccepted,
                DateChoose = request.DateChoose,
                DescriptionService = request.DescriptionService,
                TotalValue = request.TotalValue,
                OrderServiceId = request.OrderServiceId
            }, cancellationToken);

            await mediator.Send(new SetBudgetOrderServiceStatusCommand()
            {
                OrderServiceId = request.OrderServiceId
            }, cancellationToken);

            return new CreateBudgetResult()
            {
                Id = response.Id,
                Status = response.Status,
                DateAccepted = response.DateAccepted,
                DateChoose = response.DateChoose,
                DescriptionService = response.DescriptionService,
                TotalValue = response.TotalValue,
                OrderServiceId = response.OrderServiceId
            };
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new CreateBudgetResult();
        }
    }
}