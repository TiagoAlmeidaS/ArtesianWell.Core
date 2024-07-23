using Domain.SeedWork;

namespace Domain.Entities.Service;

public class ServiceEntity: AggregateRoot
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Available { get; set; }
}