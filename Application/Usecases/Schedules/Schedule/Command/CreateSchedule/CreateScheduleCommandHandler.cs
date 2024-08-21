using System.Net;
using Domain.Entities.Schedules;
using Domain.Repositories;
using MediatR;
using Shared.Messages;

namespace Application.Usecases.Schedules.Schedule.Command.CreateSchedule;

public class CreateScheduleCommandHandler(IScheduleRepository repository, IMessageHandlerService msg): IRequestHandler<CreateScheduleCommand, CreateScheduleResult>
{
    public async Task<CreateScheduleResult> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await repository.Insert(new ScheduleEntity()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId
            }, cancellationToken);

            return new CreateScheduleResult();
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new CreateScheduleResult();
        }
    }
}