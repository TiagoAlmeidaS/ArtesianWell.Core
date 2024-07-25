using System.Net;
using Application.Usecases.OrderService.Command.ChangeStatusOrderService;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.SetServiceScheduleDate;

public class SetServiceScheduleDateCommandHandler(IOrderServiceRepository orderServiceRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<SetServiceScheduleDateCommand, SetServiceScheduleDateResult>
{
    public async Task<SetServiceScheduleDateResult> Handle(SetServiceScheduleDateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicesEntities = await orderServiceRepository.GetWhere(x => x.Id == request.OrderServiceId, cancellationToken);
            
            if (!servicesEntities.Any())
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorOrderServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .Commit();
                
                return new SetServiceScheduleDateResult();
            }
            
            var serviceEntity = servicesEntities.First();
            
            serviceEntity.ServiceSchedulingDate = request.ServiceSchedulingDate;
            
            var response = await orderServiceRepository.Update(serviceEntity, cancellationToken);

            mediator.Send(new ChangeStatusOrderServiceCommand()
            {
                Id = serviceEntity.ServiceId,
                Status = StatusOrderEnum.DataSelecionada.ToString()
            });
            
            return new SetServiceScheduleDateResult {
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

            return new SetServiceScheduleDateResult();
        }
    }
}