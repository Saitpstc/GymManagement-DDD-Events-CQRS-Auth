namespace IntegrationEvents.CustomerModule;

using Shared.Core.Domain;

/// <summary>
///     Invoice Service Will Subscribe to this event to create invoice for customer
/// </summary>
public record MembershipCreatedEvent:IntegrationEvent
{

    public Guid CustomerId { get; set; }

    public DateTime MembershipStartDate { get; set; }
    public DateTime MembershipEndDate { get; set; }
}