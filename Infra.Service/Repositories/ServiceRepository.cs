using System.Linq.Expressions;
using System.Net;
using Application.Interfaces;
using Domain.Entities.Service;
using Domain.Repositories;
using Infra.Service.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Messages;

namespace Infra.Service.Repositories;

public class ServiceRepository(IMessageHandlerService msg, ServiceDBContext context, IUnitOfWork unitOfWork): IServiceRepository
{
    private DbSet<ServiceEntity> serviceDb => context.Services;
    public async Task<ServiceEntity> Insert(ServiceEntity aggregate, CancellationToken cancellationToken)
    {
        var existingCustomer = await serviceDb.FirstOrDefaultAsync(c => c.Id == aggregate.Id);
        if (existingCustomer != null)
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorServiceAlreadyExists)
                .WithStatusCode(HttpStatusCode.BadRequest)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
            
            return null;
        }
        
        var response = await serviceDb.AddAsync(aggregate, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        
        return response.Entity;
    }

    public async Task<IList<ServiceEntity>> GetWhere(Expression<Func<ServiceEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        try
        {
            return await serviceDb.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            msg.AddError()
                .WithMessage(MessagesConsts.ErrorServiceNotFound)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .WithStackTrace(e.StackTrace)
                .Commit();

            return new List<ServiceEntity>();
        }
    }

    public async Task<ServiceEntity> Update(ServiceEntity aggregate, CancellationToken cancellationToken)
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
        
        entity.Title = aggregate.Title;
        entity.Description = aggregate.Description;
        entity.Available = aggregate.Available;

        serviceDb.Update(entity);
        var response  = serviceDb.Update(entity);
        await unitOfWork.Commit(cancellationToken);
        
        return response.Entity;
    }

    public async Task Delete(string id, CancellationToken cancellationToken)
    {
        var entity = serviceDb.Find(id);
        if (entity is null)
        {
            msg
                .AddError()
                .WithMessage(MessagesConsts.ErrorServiceNotFound)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
        }
        
        var response = serviceDb.Remove(entity);
        await unitOfWork.Commit(cancellationToken);
    }

    public Task<IEnumerable<ServiceEntity>> GetAll(CancellationToken cancellationToken)
    {
        var entity = serviceDb.AsNoTracking().AsEnumerable();
        if (!entity.Any())
        {
            msg
                .AddError()
                .WithMessage(MessagesConsts.ErrorServiceNotFound)
                .WithStatusCode(HttpStatusCode.InternalServerError)
                .WithErrorCode(Guid.NewGuid().ToString())
                .Commit();
        }
        
        return Task.FromResult(entity);
    }
}