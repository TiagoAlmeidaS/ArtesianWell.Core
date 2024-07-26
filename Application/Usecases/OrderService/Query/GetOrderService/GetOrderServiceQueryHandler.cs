using System.Net;
using Application.Usecases.OrderStatus.Query.GetOrderStatus;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.OrderService.Query.GetOrderService;

public class GetOrderServiceQueryHandler(IOrderServiceRepository orderServiceRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<GetOrderServiceQuery, GetOrderServiceResult>
{
    public async Task<GetOrderServiceResult> Handle(GetOrderServiceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderServices = await orderServiceRepository.GetWhere(x => x.Id == request.OrderId, cancellationToken);
            if (!orderServices.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorOrderServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new GetOrderServiceResult();
            }

            var orderService = orderServices.First();
            var orderStatus = await mediator.Send(new GetOrderStatusQuery()
            {
                OrderStatusId = int.Parse(orderService.Status)
            }, cancellationToken);
            
            return new ()
            {
                Id = orderService.Id,
                Status = orderStatus.Name,
                ServiceId = orderService.ServiceId,
                ClientId = orderService.ClientId,
                BudgetSchedulingDate = orderService.BudgetSchedulingDate,
                ServiceSchedulingDate = orderService.ServiceSchedulingDate
            };
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();
                
            return new GetOrderServiceResult();
        }
    }
}