using System.Net;
using Application.Usecases.OrderStatus.Query.GetAllOrderStatus;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.OrderService.Query.GetOrderServiceByClient;

public class GetOrderServiceByClientQueryHandler(IOrderServiceRepository orderServiceRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<GetOrderServiceByClientQuery, List<GetOrderServiceByClientResult>>
{
    public async Task<List<GetOrderServiceByClientResult>> Handle(GetOrderServiceByClientQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderServices = await orderServiceRepository.GetWhere(x => x.ClientId == request.ClientId, cancellationToken);
            if (!orderServices.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorOrderServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new List<GetOrderServiceByClientResult>();
            }

            var orderStatus = await mediator.Send(new GetAllOrderStatusQuery(), cancellationToken);
            
            return orderServices.Select(orderService =>
            {
                var status = orderStatus.FirstOrDefault(x => x.Id.ToString() == orderService.Status);
                return new GetOrderServiceByClientResult()
                {
                    Id = orderService.Id,
                    Status = status?.Name,
                    ServiceId = orderService.ServiceId,
                    ClientId = orderService.ClientId,
                    BudgetSchedulingDate = orderService.BudgetSchedulingDate,
                    ServiceSchedulingDate = orderService.ServiceSchedulingDate
                };
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
                
            return new List<GetOrderServiceByClientResult>();
        }
    }
}