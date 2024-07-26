using System.Net;
using Application.Usecases.OrderStatus.Query.GetOrderStatus;
using Domain.Entities.OrderService;
using Domain.Repositories;
using MediatR;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.CreateOrderService;

public class CreateOrderServiceCommandHandler(IOrderServiceRepository orderServiceRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<CreateOrderServiceCommand, CreateOrderServiceResult>
{
    public async Task<CreateOrderServiceResult> Handle(CreateOrderServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderStatus = await mediator.Send(new GetOrderStatusQuery()
            {
                Status = StatusOrderConst.GetNameWithType(StatusOrderEnum.Solicitado)
            }, cancellationToken);
            
            var response = await orderServiceRepository.Insert(new OrderServiceEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Status = orderStatus.Id.ToString(),
                ClientId = request.ClientId,
                ServiceId = request.ServiceId,
                BudgetSchedulingDate = request.BudgetSchedulingDate.ToUniversalTime(),
                ServiceSchedulingDate = DateTime.MinValue
            }, cancellationToken);

            return new CreateOrderServiceResult()
            {
                Id = response.Id,
                ServiceId = response.ServiceId,
                BudgetSchedulingDate = response.BudgetSchedulingDate,
                ServiceSchedulingDate = response.ServiceSchedulingDate,
                ClientId = response.ClientId,
                Status = response.Status
            };
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new CreateOrderServiceResult();
        }
    }
}