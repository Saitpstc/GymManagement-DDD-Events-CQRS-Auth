namespace Shared.Core;

public interface IRepository<T> where T: AggregateRoot
{
    Task<T> RetriveBy(Guid Id);

    Task Update(AggregateRoot Aggregate);

    Task DeleteBy(Guid Id);

    Task Add(AggregateRoot Aggregate);
}