using Domain.SeedWork;

namespace Domain.Entities.Schedules;

public class ScheduleEntity: AggregateRoot
{
    public string Id { get; set; }
    public string UserId { get; set; }
}