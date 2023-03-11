namespace DomainEvents.CustomerModule;

using Shared.Core;

/// <summary>
///     Invoice Service Will Subscribe to this event to create invoice for customer
/// </summary>
public record MembershipCreatedEvent:DomainEvent
{

    public Guid CustomerId { get; set; }
    public string MembershipType { get; set; }
    public DateTime MembershipStartDate { get; set; }
    public DateTime MembershipEndDate { get; set; }
}