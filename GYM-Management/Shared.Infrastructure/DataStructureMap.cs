namespace Shared.Infrastructure;

using Core;

public abstract class DataStructureMap<T> where T: AggregateRoot
{
    protected abstract T MapToAggregate(DataStructureBase DatabaseTable);

    protected abstract DataStructureBase MapToDatabaseTable(T Aggregate);
}