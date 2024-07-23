namespace Domain.SeedWork.GenericRepositories;

public interface IGetAllRepository<TAggregate> : IRepository
    where TAggregate : AggregateRoot
{
    public Task<IEnumerable<TAggregate>> GetAll(CancellationToken cancellationToken);
}