using System.Linq.Expressions;
using System.Net;
using Application.Interfaces;
using Domain.Entities.Budget;
using Domain.Repositories;
using Infra.Service.Configurations;
using Infra.Service.Context;
using Infra.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Messages;

namespace Infra.Service.Repositories;

public class BudgetRepository(ServiceDBContext context, IMessageHandlerService msg, IUnitOfWork unitOfWork): IBudgetRepository
{
    private DbSet<BudgetEntity> serviceDb => context.Budgets;
    
    public async Task<BudgetEntity> Insert(BudgetEntity aggregate, CancellationToken cancellationToken)
    {
        var response = await serviceDb.AddAsync(aggregate, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        return response.Entity;
    }

    public async Task<IList<BudgetEntity>> GetWhere(Expression<Func<BudgetEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        try
        {
            return await serviceDb.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorBudgetNotFound)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new List<BudgetEntity>();
        }
    }

    public async Task<BudgetEntity> Update(BudgetEntity aggregate, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await serviceDb.FindAsync(aggregate.Id);
            if (entity is null)
            {
                msg
                    .AddError()
                    .WithMessage(MessagesConsts.ErrorServiceNotFound)
                    .WithStatusCode(HttpStatusCode.InternalServerError)
                    .WithErrorCode(Guid.NewGuid().ToString())
                    .Commit();
            }

            entity.UpdatedAt = DateTime.Now;
            entity.Status = aggregate.Status;
            entity.DateAccepted = aggregate.DateAccepted;
            entity.DateChoose = aggregate.DateChoose;
            entity.DescriptionService = aggregate.DescriptionService;
            entity.TotalValue = aggregate.TotalValue;
        
            var response  = serviceDb.Update(entity);
            await unitOfWork.Commit(cancellationToken);
        
            return response.Entity;
        }
        catch (Exception e)
        {
            msg
                .AddError()
                .WithMessage(e.Message)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();

            return null;
        }
    }
}