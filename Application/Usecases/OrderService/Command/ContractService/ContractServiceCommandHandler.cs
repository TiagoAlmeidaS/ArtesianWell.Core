using System.Net;
using Application.Usecases.OrderService.Command.ChangeStatusOrderService;
using Application.Usecases.OrderStatus.Query.GetOrderStatus;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Consts;
using Shared.Messages;

namespace Application.Usecases.OrderService.Command.ContractService;

public class ContractServiceCommandHandler(IMediator mediator, IMessageHandlerService msg): IRequestHandler<ContractServiceCommand, ContractServiceResult>
{
    public async Task<ContractServiceResult> Handle(ContractServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderStatus = await mediator.Send(new GetOrderStatusQuery()
            {
                Status = StatusOrderConst.GetNameWithType(StatusOrderEnum.DataSelecionada)
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
                
                return new ContractServiceResult();
            }
            
            return new ContractServiceResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithStackTrace(e.StackTrace)
                .Commit();
            
            return new ContractServiceResult();
        }
    }
}