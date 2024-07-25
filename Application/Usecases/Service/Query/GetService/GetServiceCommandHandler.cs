using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Service.Query.GetService;

public class GetServiceCommandHandler(IServiceRepository serviceRepository, IMessageHandlerService msg): IRequestHandler<GetServiceCommand, GetServiceResult>
{
    public async Task<GetServiceResult> Handle(GetServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicesEntities = await serviceRepository.GetWhere(x => x.Id == request.ServiceId, cancellationToken);
            
            if (!servicesEntities.Any())
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .Commit();
                return new GetServiceResult();
            }
            
            var serviceEntity = servicesEntities.First();
            
            return new GetServiceResult()
            {
                Title = serviceEntity.Title,
                Description = serviceEntity.Description,
                CreatedDate = serviceEntity.CreatedAt,
                Available = serviceEntity.Available,
                Id = serviceEntity.Id
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

            return new GetServiceResult();
        }
    }
}