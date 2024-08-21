using System.Net;
using Domain.Entities.Schedules;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Schedules.Event.Query.GetEvent;

public class GetEventQueryHandler(IEventRepository eventRepository, IMessageHandlerService msg): IRequestHandler<GetEventQuery, List<GetEventResult>>
{
    public async Task<List<GetEventResult>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        try
        {
            
            IList<EventEntity> scheduleEntities;

            if (request.Ids != null && request.Ids.Any())
            {
                scheduleEntities = await eventRepository.GetWhere(x => request.Ids.Contains(x.Id), cancellationToken);
            }
            else
            {
                scheduleEntities = await eventRepository.GetWhere(x => x.Id == request.Id, cancellationToken);
            }
            
            if (!scheduleEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorScheduleNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new ();
            }

            return scheduleEntities.Select(eventEntity => new GetEventResult()
            {
                ScheduleId = eventEntity.ScheduleId,
                Id = eventEntity.Id,
                Date = eventEntity.Date,
                Description = eventEntity.Description,
                EndTime = eventEntity.EndTime,
                StartTime = eventEntity.StartTime
            }).ToList();


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