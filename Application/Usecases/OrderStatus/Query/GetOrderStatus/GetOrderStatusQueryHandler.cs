using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.OrderStatus.Query.GetOrderStatus;

public class GetOrderStatusQueryHandler(IOrderStatusRepository statusRepository, IMessageHandlerService msg): IRequestHandler<GetOrderStatusQuery, GetOrderStatusResult>
{
    public async Task<GetOrderStatusResult> Handle(GetOrderStatusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderStatusEntities = await statusRepository.GetWhere(x => x.Id == request.OrderStatusId, cancellationToken);
            if (!orderStatusEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorOrderStatusNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new GetOrderStatusResult();
            }

            var orderStatusEntity = orderStatusEntities.First();
            
            return new ()
            {
                Id = orderStatusEntity.Id,
                Description = orderStatusEntity.Description,
                Name = orderStatusEntity.Name,
                PossibleActions = orderStatusEntity.PossibleActions
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
                
            return new GetOrderStatusResult();
        }
    }
}