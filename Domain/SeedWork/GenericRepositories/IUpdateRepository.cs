namespace Domain.SeedWork.GenericRepositories;

public interface IUpdateRepository<TAggregate>: IRepository
    where TAggregate : AggregateRoot
{
    public Task<TAggregate> Update(TAggregate aggregate, CancellationToken cancellationToken);
}