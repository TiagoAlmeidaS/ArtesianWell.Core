using System.Net;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Service.Command.UpdateService;

public class UpdateServiceCommandHandler(IServiceRepository serviceRepository, IMessageHandlerService msg): IRequestHandler<UpdateServiceCommand, UpdateServiceResult>
{
    public async Task<UpdateServiceResult> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicesEntities = await serviceRepository.GetWhere(x => x.Id == request.Id, cancellationToken);
            
            if (!servicesEntities.Any())
            {
                msg.AddError()
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .WithMessage(MessagesConsts.ServiceNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .Commit();
                return new UpdateServiceResult();
            }
            
            var serviceEntity = servicesEntities.First();
            
            serviceEntity.Title = request.Title;
            serviceEntity.Description = request.Description;
            serviceEntity.Available = request.Available;
            
            var response = await serviceRepository.Update(serviceEntity, cancellationToken);
            
            return new UpdateServiceResult(){
                Title = response.Title,
                Description = response.Description,
                CreatedDate = response.CreatedAt,
                Available = response.Available,
                Id = response.Id
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

            return new UpdateServiceResult();
        }
    }
}