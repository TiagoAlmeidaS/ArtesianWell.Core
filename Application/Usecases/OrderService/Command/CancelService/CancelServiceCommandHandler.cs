using System.Net;
using Application.Usecases.OrderService.Command.ChangeStatusOrderService;
using Application.Usecases.OrderStatus.Query.GetOrderStatus;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.CancelService;

public class CancelServiceCommandHandler(IMessageHandlerService msg, IMediator mediator): IRequestHandler<CancelServiceCommand, CancelServiceResult>
{
    public async Task<CancelServiceResult> Handle(CancelServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderStatus = await mediator.Send(new GetOrderStatusQuery()
            {
                Status = StatusOrderConst.GetNameWithType(StatusOrderEnum.Cancelado)
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
                
                return new CancelServiceResult();
            }
            
            return new CancelServiceResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithStackTrace(e.StackTrace)
                .Commit();
            
            return new CancelServiceResult();
        }
    }
}