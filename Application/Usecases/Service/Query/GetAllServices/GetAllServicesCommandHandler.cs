using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Service.Query.GetAllServices;

public class GetAllServicesCommandHandler(IServiceRepository serviceRepository, IMessageHandlerService msg): IRequestHandler<GetAllServicesCommand, List<GetAllServicesResult>>
{
    public async Task<List<GetAllServicesResult>> Handle(GetAllServicesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicesEntities = await serviceRepository.GetAll(cancellationToken);
            
            if (!servicesEntities.Any())
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .Commit();
                return new List<GetAllServicesResult>();
            }

            return servicesEntities.Select(serviceEntity => new GetAllServicesResult()
            {
                Title = serviceEntity.Title,
                Description = serviceEntity.Description,
                CreatedDate = serviceEntity.CreatedAt,
                Available = serviceEntity.Available,
                Id = serviceEntity.Id
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

            return new List<GetAllServicesResult>();
        }
    }
}