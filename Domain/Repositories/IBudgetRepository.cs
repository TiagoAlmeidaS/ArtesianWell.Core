using Domain.Entities.Budget;
using Domain.SeedWork.GenericRepositories;

namespace Domain.Repositories;

public interface IBudgetRepository: IInsertRepository<BudgetEntity>, IGetWhereRepository<BudgetEntity>, IUpdateRepository<BudgetEntity>
{
}