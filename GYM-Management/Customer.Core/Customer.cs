namespace Customer.Core;

using DomainEvents.CustomerModule;
using ValueObjects;

internal class Customer:AggregateRoot
{
    private Name Name;
    private Membership Membership;
    private PhoneNumber PhoneNumber;
    private Email Email;
    private int _totalMonthsOfMembership;


    public Customer(Name name, PhoneNumber phoneNumber, Email email)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public void StartMembership(Membership membership)
    {
        Membership = membership;
        _totalMonthsOfMembership = membership.TimePeriodInMonths();

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

    public Membership GetMembership() => Membership;

    public PhoneNumber GetNumber() => PhoneNumber;

    public int GetTotalMembershipInMonths() => _totalMonthsOfMembership;
}