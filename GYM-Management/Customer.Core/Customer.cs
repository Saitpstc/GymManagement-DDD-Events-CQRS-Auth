namespace Customer.Core;

using IntegrationEvents.CustomerModule;
using Shared.Core.Domain;
using ValueObjects;

public class Customer:AggregateRoot
{
    private readonly Email Email;
    private Membership? Membership;
    private readonly Name Name;
    private readonly PhoneNumber PhoneNumber;




    public Customer(Name name, PhoneNumber phoneNumber, Email email, Membership? membership = null)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Membership = membership;
    }

    public Guid UserId { get; set; }

    public void StartMembership(Membership membership)
    {
        Membership = membership;

        MembershipCreatedEvent customerCreatedEvent = new MembershipCreatedEvent
        {
            EventId = new Guid(),
            CustomerId = Id,
            MembershipType = membership.SubscriptionType().ToString(),
            MembershipStartDate = membership.StartedAt(),
            MembershipEndDate = membership.EndsAt()
        };
        Apply(customerCreatedEvent);
    }

    public Email GetMail()
    {
        return Email;
    }

    public Name GetName()
    {
        return Name;
    }

    public Membership? GetMembership()
    {
        return Membership;
    }

    public PhoneNumber GetNumber()
    {
        return PhoneNumber;
    }
}