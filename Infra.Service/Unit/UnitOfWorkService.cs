using Application.Interfaces;
using Infra.Service.Context;

namespace Infra.Service.Unit;

public class UnitOfWorkService(ServiceDBContext _context): IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken) =>
        _context.Database.RollbackTransactionAsync(cancellationToken);
}