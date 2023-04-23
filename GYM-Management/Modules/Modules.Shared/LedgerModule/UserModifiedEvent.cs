namespace IntegrationEvents.LedgerModule;

using Shared.Core.Domain;

public record UserModifiedEvent(string StripeId, string UserId):IntegrationEvent
{
}