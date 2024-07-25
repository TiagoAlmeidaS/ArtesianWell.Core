using System.Linq.Expressions;
using System.Net;
using Application.Interfaces;
using Domain.Entities.OrderStatus;
using Domain.Repositories;
using Infra.Service.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Messages;

namespace Infra.Service.Repositories;

public class OrderStatusRepository(IMessageHandlerService msg, ServiceDBContext context, IUnitOfWork unitOfWork): IOrderStatusRepository
{
    private DbSet<OrderStatusEntity> serviceDb => context.OrderStatus;
    public Task<IEnumerable<OrderStatusEntity>> GetAll(CancellationToken cancellationToken)
    {
        var entity = serviceDb.AsNoTracking().AsEnumerable();
        if (!entity.Any())
        {
            msg
                .AddError()
                .WithMessage(MessagesConsts.ErrorOrderStatusNotFound)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
        }
        
        return Task.FromResult(entity);
    }

    public async Task<IList<OrderStatusEntity>> GetWhere(Expression<Func<OrderStatusEntity, bool>> predicate, CancellationToken cancellationToken)
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

            return new List<OrderStatusEntity>();
        }
    }
}