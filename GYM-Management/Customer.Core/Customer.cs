namespace Customer.Core;

using IntegrationEvents.CustomerModule;
using Shared.Core.Domain;
using Shared.Core.Exceptions;
using ValueObjects;

internal class Customer:AggregateRoot
{
    public Email Email { get; private set; }
    public Membership? Membership { get; private set; }
    public int TotalMonthsOfMembership { get; private set; }
    public ICollection<InvoiceId> Bills { get; private set; }
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
        TotalMonthsOfMembership += membership.TimePeriodInMonths();

    }

    public void ExtendMembership(DateTime endDate)
    {
        if (Membership is null)
        {
            throw new DomainValidationException("Customer does not have a membership please create new membership");
        }
        TotalMonthsOfMembership += ExtendedPeriodInMonths(endDate);

        Membership = Membership.Extend(endDate);

    }

    private int ExtendedPeriodInMonths(DateTime EndDate)
    {

        TimeSpan result = EndDate.Subtract(Membership.StartDate);
        var totalMonths = Math.Round(result.TotalDays / 30.44);
        return (int) totalMonths;

    }

    public void TerminateMembership()
    {
        Membership = null;
    }


}