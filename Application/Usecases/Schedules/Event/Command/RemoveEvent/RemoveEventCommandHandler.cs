using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Messages;

namespace Application.Usecases.Schedules.Event.Command.RemoveEvent;

public class RemoveEventCommandHandler(IEventRepository eventRepository, IMessageHandlerService msg): IRequestHandler<RemoveEventCommand, RemoveEventResult>
{
    public async Task<RemoveEventResult> Handle(RemoveEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await eventRepository.Delete(request.Id, cancellationToken);
            return new ();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();
                
            return new ();
        }
    }
}