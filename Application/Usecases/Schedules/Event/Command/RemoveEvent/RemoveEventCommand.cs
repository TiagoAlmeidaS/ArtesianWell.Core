using MediatR;

namespace Application.Usecases.Schedules.Event.Command.RemoveEvent;

public class RemoveEventCommand : IRequest<RemoveEventResult>
{
    public string Id { get; set; }
}