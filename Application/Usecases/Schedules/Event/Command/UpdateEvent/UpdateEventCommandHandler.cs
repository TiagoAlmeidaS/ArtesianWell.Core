using System.Net;
using Application.Usecases.Schedules.Event.Query.GetEvent;
using Domain.Entities.Schedules;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Schedules.Event.Command.UpdateEvent;

public class UpdateEventCommandHandler(IEventRepository eventRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<UpdateEventCommand, UpdateEventResult>
{
    public async Task<UpdateEventResult> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var eventResults = await mediator.Send(new GetEventQuery()
            {
                Id = request.Id
            });
            
            if (!eventResults.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorEventNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new ();
            }
            
            var eventEntity = eventResults.First();
            
            var updatedEvent = await eventRepository.Update(new EventEntity()
            {
                Id = eventEntity.Id,
                ScheduleId = eventEntity.ScheduleId,
                Date = eventEntity.Date,
                Description = eventEntity.Description,
                EndTime = eventEntity.EndTime,
                StartTime = eventEntity.StartTime,
            }, cancellationToken);
            
            
            return new UpdateEventResult()
            {
                Id = updatedEvent.Id,
                ScheduleId = updatedEvent.ScheduleId,
                Date = updatedEvent.Date,
                Description = updatedEvent.Description,
                EndTime = updatedEvent.EndTime,
                StartTime = updatedEvent.StartTime
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
                
            return new ();
        }
    }
}