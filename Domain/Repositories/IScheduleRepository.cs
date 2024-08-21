using Domain.Entities.Schedules;
using Domain.SeedWork.GenericRepositories;

namespace Domain.Repositories;

public interface IScheduleRepository: IInsertRepository<ScheduleEntity>, IGetWhereRepository<ScheduleEntity>
{
    
}