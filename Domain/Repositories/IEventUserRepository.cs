using Domain.Entities.Schedules;
using Domain.SeedWork.GenericRepositories;

namespace Domain.Repositories;

public interface IEventUserRepository: IInsertRepository<EventUserEntity>, IGetWhereRepository<EventUserEntity>, IUpdateRepository<EventUserEntity>, IDeleteRepository<EventUserEntity>
{
    
}