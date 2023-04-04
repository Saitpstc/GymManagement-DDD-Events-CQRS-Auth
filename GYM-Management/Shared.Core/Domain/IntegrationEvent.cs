namespace Shared.Core.Domain;

using MediatR;

public record IntegrationEvent:INotification
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime OccuredOn { get; } = DateTime.Now;
}