namespace Shared.Core;

public record DomainEvent
{
    public Guid EventId { get; init; }
    public DateTime OccuredOn { get; } = DateTime.Now;
}