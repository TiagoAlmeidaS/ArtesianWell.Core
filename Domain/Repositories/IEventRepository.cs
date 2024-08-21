using Domain.Entities.Schedules;
using Domain.SeedWork.GenericRepositories;

namespace Domain.Repositories;

public interface IEventRepository: IInsertRepository<EventEntity>, IGetWhereRepository<EventEntity>, IUpdateRepository<EventEntity>, IDeleteRepository<EventEntity>
{
    
}