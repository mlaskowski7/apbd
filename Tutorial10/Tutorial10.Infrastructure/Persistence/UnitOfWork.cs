using Microsoft.EntityFrameworkCore.Storage;
using Tutorial10.Application.Persistence;
using Tutorial10.Infrastructure.Database;

namespace Tutorial10.Infrastructure.Persistence;

public class UnitOfWork(ClinicDbContext dbContext) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    
    public async Task BeginAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        } 
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}