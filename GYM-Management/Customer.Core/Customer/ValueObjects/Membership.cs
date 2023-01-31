namespace Customer.Core.Customer.ValueObjects.CustMembership;

using Enums;
using Exceptions.MembershipExceptions;

public record Membership:ValueObject
{

    private readonly SubscriptionEnum _membershipType;
    private readonly DateOnly _startDate;
    private readonly DateOnly _endDate;


    private Membership(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
        {
            throw new MembershipException();
        }
        if (startDate < DateOnly.FromDateTime(DateTime.Now)) throw new MembershipException();
        _startDate = startDate;
        _endDate = endDate;
        _membershipType = SubscriptionEnum.Custom;
    }

    private Membership(SubscriptionEnum membershipType)
    {
        var now = DateTime.Now;
        _startDate = DateOnly.FromDateTime(now);
        _endDate = DateOnly.FromDateTime(now.AddMonths((int) membershipType));
        _membershipType = membershipType;
    }

    public DateOnly StartedAt()
    {
        return _startDate;
    }

    public DateOnly EndsAt()
    {
        return _endDate;
    }

    public int TimePeriod()
    {
        return (int) _membershipType;
    }

    public static Membership Monthly()
    {
        return new Membership(SubscriptionEnum.Monthly);
    }

    public static Membership Quarterly()
    {
        return new Membership(SubscriptionEnum.Quarterly);
    }

    public static Membership HalfYear()
    {
        return new Membership(SubscriptionEnum.HalfYear);
    }

    public static Membership Yearly()
    {
        return new Membership(SubscriptionEnum.Yearly);
    }

    public static Membership Custom(DateOnly startDate, DateOnly endDate)
    {
        return new Membership(startDate, endDate);
    }

}