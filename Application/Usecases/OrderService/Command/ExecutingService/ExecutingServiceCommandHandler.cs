using System.Net;
using Application.Usecases.OrderService.Command.ChangeStatusOrderService;
using Application.Usecases.OrderService.Command.ContractService;
using Application.Usecases.OrderStatus.Query.GetOrderStatus;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.ExecutingService;

public class ExecutingServiceCommandHandler(IMediator mediator, IMessageHandlerService msg): IRequestHandler<ExecutingServiceCommand, ExecutingServiceResult>
{
    public async Task<ExecutingServiceResult> Handle(ExecutingServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderStatus = await mediator.Send(new GetOrderStatusQuery()
            {
                Status = StatusOrderConst.GetNameWithType(StatusOrderEnum.EmExecução)
            }, cancellationToken);
            
            var orderService = mediator.Send(new ChangeStatusOrderServiceCommand()
            {
                Id = request.OrderServiceId,
                Status = orderStatus.Id.ToString()
            }, cancellationToken).Result;
            
            if (orderService == null)
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorDefault)
                    .WithStatusCode(HttpStatusCode.InternalServerError)
                    .Commit();
                
                return new ExecutingServiceResult();
            }
            
            return new ExecutingServiceResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithStackTrace(e.StackTrace)
                .Commit();
            
            return new ExecutingServiceResult();
        }
    }
}