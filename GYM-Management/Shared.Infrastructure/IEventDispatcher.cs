namespace Shared.Infrastructure;

using Core;

public interface IEventDispatcher
{
    Task Dispatch(List<DomainEvent> events);
}
