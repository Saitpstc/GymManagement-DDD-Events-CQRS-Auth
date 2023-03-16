namespace Shared.Core.Contracts;

using Domain;

public interface IRepository<T> where T: AggregateRoot
{
    Task<T?> RetriveByAsync(Guid Id);

    Task<bool> UpdateAsync(T Aggregate);

    Task DeleteByAsync(T Aggregate);

    Task<T> AddAsync(T Aggregate);

    Task<IEnumerable<T>> GetAllAsync(T Aggregate);

    Task<int> CommitAsync();
}