using Domain.Entities.OrderService;
using Domain.SeedWork.GenericRepositories;

namespace Domain.Repositories;

public interface IOrderServiceRepository: IInsertRepository<OrderServiceEntity>, IGetWhereRepository<OrderServiceEntity>, IUpdateRepository<OrderServiceEntity>
{
}