using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Messages;

namespace Application.Usecases.Service.Command.RemoveService;

public class RemoveServiceCommandHandler(IServiceRepository serviceRepository, IMessageHandlerService msg): IRequestHandler<RemoveServiceCommand, RemoveServiceResult>
{
    public async Task<RemoveServiceResult> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await serviceRepository.Delete(request.ServiceId, cancellationToken);
            return new RemoveServiceResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new RemoveServiceResult();
        }
    }
}