using Domain.Entities.OrderStatus;
using Domain.SeedWork.GenericRepositories;

namespace Domain.Repositories;

public interface IOrderStatusRepository: IGetAllRepository<OrderStatusEntity>, IGetWhereRepository<OrderStatusEntity>
{
}