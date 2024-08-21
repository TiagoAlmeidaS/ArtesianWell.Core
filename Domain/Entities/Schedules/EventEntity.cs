

using Domain.SeedWork;

namespace Domain.Entities.Schedules;

public class EventEntity: AggregateRoot
{
    public string Id { get; set; }
    public string ScheduleId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}