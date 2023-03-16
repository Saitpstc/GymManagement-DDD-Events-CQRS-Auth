namespace Shared.Infrastructure.Database;

using Core.Domain;

public interface DataStructureMap<T, Y> where T: DataStructureBase where Y: AggregateRoot
{
    Y MapToAggregate(T DatabaseTable);

    T MapToDatabaseTable(Y Aggregate);
}