namespace Tutorial10.Application.Persistence;

public interface IUnitOfWork
{
    Task BeginAsync(CancellationToken cancellationToken = default);
    
    Task CommitAsync(CancellationToken cancellationToken = default);
    
    Task RollbackAsync(CancellationToken cancellationToken = default);
}