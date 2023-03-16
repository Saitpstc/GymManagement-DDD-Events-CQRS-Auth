namespace Shared.Core.Domain;

using MediatR;

public record DomainEvent:INotification
{
    public Guid EventId { get; init; }
    public DateTime OccuredOn { get; } = DateTime.Now;
}