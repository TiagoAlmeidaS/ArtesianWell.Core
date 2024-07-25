using System.Net;
using Domain.Entities.OrderService;
using Domain.Repositories;
using MediatR;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.CreateOrderService;

public class CreateOrderServiceCommandHandler(IOrderServiceRepository orderServiceRepository, IMessageHandlerService msg): IRequestHandler<CreateOrderServiceCommand, CreateOrderServiceResult>
{
    public async Task<CreateOrderServiceResult> Handle(CreateOrderServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await orderServiceRepository.Insert(new OrderServiceEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Status = request.Status,
                ClientId = request.ClientId,
                ServiceId = request.ServiceId,
                BudgetSchedulingDate = request.BudgetSchedulingDate.ToUniversalTime(),
                ServiceSchedulingDate = request.ServiceSchedulingDate.ToUniversalTime()
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