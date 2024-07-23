namespace Domain.SeedWork.GenericRepositories;

public interface IInsertRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot
{
    public Task<TAggregate> Insert(TAggregate aggregate, CancellationToken cancellationToken);
}