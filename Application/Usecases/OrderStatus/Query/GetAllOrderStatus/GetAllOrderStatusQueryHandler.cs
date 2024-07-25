using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.OrderStatus.Query.GetAllOrderStatus;

public class GetAllOrderStatusQueryHandler(IOrderStatusRepository statusRepository, IMessageHandlerService msg): IRequestHandler<GetAllOrderStatusQuery, List<GetAllOrderStatusResult>>
{
    public async Task<List<GetAllOrderStatusResult>> Handle(GetAllOrderStatusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderStatusEntities = await statusRepository.GetAll(cancellationToken);
            
            if (!orderStatusEntities.Any())
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ErrorOrderStatusNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .Commit();
                return new List<GetAllOrderStatusResult>();
            }

            return orderStatusEntities.Select(orderStatusEntity => new GetAllOrderStatusResult()
            {
                Name = orderStatusEntity.Name,
                PossibleActions = orderStatusEntity.PossibleActions,
                Description = orderStatusEntity.Description,
                Id = orderStatusEntity.Id
            }).ToList();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new List<GetAllOrderStatusResult>();
        }
    }
}