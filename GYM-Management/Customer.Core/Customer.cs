namespace Customer.Core;

using DomainEvents.CustomerModule;
using ValueObjects;

public class Customer:AggregateRoot
{
    private Name Name;
    private Membership? Membership;
    private PhoneNumber PhoneNumber;
    private Email Email;
    public Guid UserId { get; set; }




    public Customer(Name name, PhoneNumber phoneNumber, Email email, Membership? membership = null)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Membership = membership;
    }

    public void StartMembership(Membership membership)
    {
        Membership = membership;

        var customerCreatedEvent = new MembershipCreatedEvent()
        {
            EventId = new Guid(),
            CustomerId = Id,
            MembershipType = membership.SubscriptionType().ToString(),
            MembershipStartDate = membership.StartedAt(),
            MembershipEndDate = membership.EndsAt()
        };
        Apply(customerCreatedEvent);
    }

    public Email GetMail() => Email;

    public Name GetName() => Name;

    public Membership? GetMembership() => Membership;

    public PhoneNumber GetNumber() => PhoneNumber;


}