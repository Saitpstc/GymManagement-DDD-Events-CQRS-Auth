namespace Customer.Core;

using IntegrationEvents.CustomerModule;
using Shared.Core.Domain;
using ValueObjects;

public class Customer:AggregateRoot
{
    public Email Email { get; private set; }
    public Membership? Membership { get; private set; }
    public Guid? MembershipId { get; private set; }
    public Name Name { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Guid UserId { get; private set; }

    private Customer()
    {

    }

    public Customer(Name name, PhoneNumber phoneNumber, Email email, Guid userId, Membership? membership = null)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        UserId = userId;
        Membership = membership;
    }



    public void StartMembership(Membership membership)
    {
        Membership = membership;

        MembershipCreatedEvent customerCreatedEvent = new MembershipCreatedEvent
        {
            EventId = new Guid(),
            CustomerId = Id,
            MembershipStartDate = membership.StartDate,
            MembershipEndDate = membership.EndDate
        };
        Apply(customerCreatedEvent);
    }


}