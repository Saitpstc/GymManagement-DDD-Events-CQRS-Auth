namespace Shared.Infrastructure;

public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken token);
}