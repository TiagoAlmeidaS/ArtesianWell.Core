using System.Net;
using Domain.Repositories;
using MediatR;
using Shared.Common;
using Shared.Messages;

namespace Application.Usecases.Schedules.Schedule.Query.GetSchedule;

public class GetScheduleQueryHandler(IScheduleRepository scheduleRepository, IMediator mediator, IMessageHandlerService msg): IRequestHandler<GetScheduleQuery, GetScheduleResult>
{
    public async Task<GetScheduleResult> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var scheduleEntities = await scheduleRepository.GetWhere(x => x.UserId == request.UserId || x.Id == request.Id, cancellationToken);
            if (!scheduleEntities.Any())
            {
                msg.AddError()
                    .WithMessage(MessagesConsts.ErrorScheduleNotFound)
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
                
                return new GetScheduleResult();
            }
            
            return new GetScheduleResult()
            {
                Id = scheduleEntities.First().Id,
                ScheduleEvents = new List<ScheduleEventResult>()
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
                
            return new GetScheduleResult();
        }
    }
}