namespace Shared.Core;

public interface IRepository<T> where T: AggregateRoot
{
    Task<T> RetriveBy(Guid Id);

    Task Update(T Aggregate);

    Task DeleteBy(Guid Id);

    Task<T> Add(T Aggregate);
}