namespace Domain.SeedWork.GenericRepositories;

public interface IDeleteRepository<TAggregate>: IRepository
    where TAggregate : AggregateRoot
{
    public Task Delete(string id, CancellationToken cancellationToken);
}