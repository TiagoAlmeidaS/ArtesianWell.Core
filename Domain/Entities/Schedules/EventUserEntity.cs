using Domain.SeedWork;

namespace Domain.Entities.Schedules;

public class EventUserEntity: AggregateRoot
{
    public string EventId { get; set; }
    public string UserId { get; set; }
}