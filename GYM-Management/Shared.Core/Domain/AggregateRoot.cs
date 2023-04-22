namespace Shared.Core.Domain;

using System.ComponentModel.DataAnnotations.Schema;
using Contracts;
using Exceptions;

public abstract class AggregateRoot:BaseEntity
{
    private readonly List<IntegrationEvent> _domainEvents = new List<IntegrationEvent>();



    [NotMapped]
    public IReadOnlyList<IntegrationEvent> DomainEvents => _domainEvents;



    protected void Apply(IntegrationEvent @event)
    {
        _domainEvents.Add(@event);
    }

    protected void RemoveEvents()
    {
        _domainEvents.Clear();
    }

    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}