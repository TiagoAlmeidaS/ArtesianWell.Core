using System.Net;
using Application.Usecases.OrderService.Command.ChangeStatusOrderService;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.SetBudgetOrderServiceStatus;

public class SetBudgetOrderServiceStatusCommandHandler(IMediator mediator, IMessageHandlerService msg): IRequestHandler<SetBudgetOrderServiceStatusCommand, SetBudgetOrderServiceStatusResult>
{
    public async Task<SetBudgetOrderServiceStatusResult> Handle(SetBudgetOrderServiceStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderService = mediator.Send(new ChangeStatusOrderServiceCommand()
            {
                Id = request.OrderServiceId,
                Status = StatusOrderEnum.OrçamentoEnviado.ToString()
            }, cancellationToken).Result;
            
            if (orderService == null)
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorDefault)
                    .WithStatusCode(HttpStatusCode.InternalServerError)
                    .Commit();
                
                return new SetBudgetOrderServiceStatusResult();
            }
            
            return new SetBudgetOrderServiceStatusResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithStackTrace(e.StackTrace)
                .Commit();
            
            return new SetBudgetOrderServiceStatusResult();
        }
    }
}