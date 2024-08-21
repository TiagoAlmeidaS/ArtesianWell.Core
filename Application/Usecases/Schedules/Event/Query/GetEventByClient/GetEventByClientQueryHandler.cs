using System.Net;
using Application.Usecases.Schedules.Event.Query.GetEvent;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Schedules.Event.Query.GetEventByClient;

public class GetEventByClientQueryHandler(IEventUserRepository eventUserRepository, IMessageHandlerService msg, IMediator mediator): IRequestHandler<GetEventByClientQuery, List<GetEventByClientResult>>
{
    public async Task<List<GetEventByClientResult>> Handle(GetEventByClientQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var eventUserEntities = await eventUserRepository.GetWhere(x => x.UserId == request.UserId, cancellationToken);
            
            if (!eventUserEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorScheduleNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new ();
            }

            var getEvents = await mediator.Send(new GetEventQuery()
            {
                Ids = eventUserEntities.Select(x => x.EventId).ToList()
            });
            
            if (!eventUserEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorScheduleNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new ();
            }

            return getEvents.Select(eventEntity => new GetEventByClientResult()
            {
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