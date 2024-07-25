using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.ChangeStatusOrderService;

public class ChangeStatusOrderServiceCommandHandler(IOrderServiceRepository orderServiceRepository, IMessageHandlerService msg): IRequestHandler<ChangeStatusOrderServiceCommand, ChangeStatusOrderServiceResult>
{
    public async Task<ChangeStatusOrderServiceResult> Handle(ChangeStatusOrderServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicesEntities = await orderServiceRepository.GetWhere(x => x.Id == request.Id, cancellationToken);
            
            if (!servicesEntities.Any())
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorOrderServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .Commit();
                
                return new ChangeStatusOrderServiceResult();
            }
            
            var serviceEntity = servicesEntities.First();
            
            serviceEntity.Status = request.Status;
            
            var response = await orderServiceRepository.Update(serviceEntity, cancellationToken);
            
            return new ChangeStatusOrderServiceResult {
                Id = response.Id,
                Status = response.Status,
                ServiceSchedulingDate = response.ServiceSchedulingDate,
                ServiceId = response.ServiceId,
                BudgetSchedulingDate = response.BudgetSchedulingDate,
                ClientId = response.ClientId
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

            return new ChangeStatusOrderServiceResult();
        }
    }
}