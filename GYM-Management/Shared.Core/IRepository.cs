namespace Shared.Core;

public interface IRepository<T> where T: AggregateRoot
{
    Task<T?> RetriveByAsync(Guid Id);

    Task<bool> UpdateAsync(T Aggregate);

    Task<bool> DeleteByAsync(T Aggregate);

    Task<T> AddAsync(T Aggregate);
    Task<IEnumerable<T>> GetAllAsync(T Aggregate);
}