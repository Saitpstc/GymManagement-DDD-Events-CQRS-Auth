namespace Shared.Infrastructure;

using Core;

public interface IUnitOfWork
{
    Task<int> CommitAsync(
        CancellationToken cancellationToken = default,
        Guid? internalCommandId = null);
}