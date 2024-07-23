namespace Domain.SeedWork.GenericRepositories;

public interface IGetRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot
{
    public Task<TAggregate> Get(int id, CancellationToken cancellationToken);
}