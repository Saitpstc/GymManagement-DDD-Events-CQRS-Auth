namespace Shared.Core;

public interface IRepository
{
    Task<AggregateRoot> RetriveBy(Guid Id);

    Task Update(AggregateRoot Aggregate);

    Task DeleteBy(Guid Id);

    Task Add(AggregateRoot Aggregate);
}