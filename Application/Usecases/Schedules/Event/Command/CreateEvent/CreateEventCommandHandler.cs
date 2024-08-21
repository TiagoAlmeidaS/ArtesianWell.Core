using System.Net;
using Domain.Entities.Schedules;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Schedules.Event.Command.CreateEvent;

public class CreateEventCommandHandler(IEventRepository eventRepository, IMessageHandlerService msg): IRequestHandler<CreateEventCommand, CreateEventResult>
{
    public async Task<CreateEventResult> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var eventEntity = await eventRepository.Insert(new EventEntity()
            {
                Id = Guid.NewGuid().ToString(),
                ScheduleId = request.ScheduleId,
                Date = request.Date,
                Description = request.Description,
                EndTime = request.EndTime,
                StartTime = request.StartTime,
            }, cancellationToken);
            
            if (string.IsNullOrEmpty(eventEntity.Id))
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorScheduleNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new ();
            }

            return new CreateEventResult()
            {
                ScheduleId = eventEntity.ScheduleId,
                Id = eventEntity.Id,
                Date = eventEntity.Date,
                Description = eventEntity.Description,
                EndTime = eventEntity.EndTime,
                StartTime = eventEntity.StartTime
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