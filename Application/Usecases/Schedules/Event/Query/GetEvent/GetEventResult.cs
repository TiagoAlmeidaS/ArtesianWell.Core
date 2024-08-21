namespace Application.Usecases.Schedules.Event.Query.GetEvent;

public class GetEventResult
{
    public string Id { get; set; }
    public string ScheduleId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}