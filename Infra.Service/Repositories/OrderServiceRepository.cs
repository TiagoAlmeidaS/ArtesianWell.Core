using System.Linq.Expressions;
using System.Net;
using Application.Interfaces;
using Domain.Entities.OrderService;
using Domain.Repositories;
using Infra.Service.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Messages;

namespace Infra.Service.Repositories;

public class OrderServiceRepository(IMessageHandlerService msg, ServiceDBContext context, IUnitOfWork unitOfWork): IOrderServiceRepository
{
    private DbSet<OrderServiceEntity> serviceDb => context.OrderServices;
    public async Task<OrderServiceEntity> Insert(OrderServiceEntity aggregate, CancellationToken cancellationToken)
    {
        var existingCustomer = await serviceDb.FirstOrDefaultAsync(c => c.Id == aggregate.Id);
        if (existingCustomer != null)
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorOrderServiceAlready)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
            
            return null;
        }
        
        var response = await serviceDb.AddAsync(aggregate, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        
        return response.Entity;
    }

    public async Task<IList<OrderServiceEntity>> GetWhere(Expression<Func<OrderServiceEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        try
        {
            return await serviceDb.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorOrderServiceNotFound)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new List<OrderServiceEntity>();
        }
    }

    public async Task<OrderServiceEntity> Update(OrderServiceEntity aggregate, CancellationToken cancellationToken)
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

        entity.Status = aggregate.Status;
        entity.BudgetSchedulingDate = aggregate.BudgetSchedulingDate;
        entity.ServiceSchedulingDate = aggregate.ServiceSchedulingDate;
        entity.UpdatedAt = DateTime.Now;
        
        var response  = serviceDb.Update(entity);
        await unitOfWork.Commit(cancellationToken);
        
        return response.Entity;
    }
}