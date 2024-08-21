namespace Application.Usecases.Schedules.Schedule.Query.GetSchedule;

public class GetScheduleResult
{
    public string Id { get; set; }
    public List<ScheduleEventResult> ScheduleEvents { get; set; }
}

public class ScheduleEventResult
{
    public string Id { get; set; }
    public string ScheduleId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}
