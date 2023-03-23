namespace Shared.Core.Domain;

using System.ComponentModel.DataAnnotations.Schema;
using Contracts;
using Exceptions;

public abstract class AggregateRoot:BaseEntity
{
    private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
    
    

    [NotMapped]
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;



    protected void Apply(DomainEvent @event)
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