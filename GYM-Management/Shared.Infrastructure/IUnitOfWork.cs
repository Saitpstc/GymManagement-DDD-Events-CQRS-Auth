namespace Shared.Infrastructure;

public interface IUnitOfWork
{
    Task<int> CommitAsync(
        CancellationToken cancellationToken = default,
        Guid? internalCommandId = null);
}