using System.Net;
using Domain.Entities.Service;
using Domain.Repositories;
using MediatR;
using Shared.Messages;

namespace Application.Usecases.Service.Command.CreateService;

public class CreateServiceCommandHandler(IMessageHandlerService msg, IServiceRepository serviceRepository): IRequestHandler<CreateServiceCommand, CreateServiceResult>
{
    public async Task<CreateServiceResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await serviceRepository.Insert(new ServiceEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Description,
                Available = request.Available
            }, cancellationToken);

            return new CreateServiceResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new CreateServiceResult();
        }
    }
}