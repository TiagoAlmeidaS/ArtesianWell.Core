using Domain.Entities.Service;
using Domain.SeedWork.GenericRepositories;

namespace Domain.Repositories;

public interface IServiceRepository: IInsertRepository<ServiceEntity>, IGetWhereRepository<ServiceEntity>, IUpdateRepository<ServiceEntity>, IDeleteRepository<ServiceEntity>, IGetAllRepository<ServiceEntity>
{
}