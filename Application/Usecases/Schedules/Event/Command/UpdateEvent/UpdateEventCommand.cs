using MediatR;

namespace Application.Usecases.Schedules.Event.Command.UpdateEvent;

public class UpdateEventCommand : IRequest<UpdateEventResult>
{
    public string Id { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}