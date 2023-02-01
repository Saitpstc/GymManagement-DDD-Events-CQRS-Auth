namespace Customer.Core.Customer.ValueObjects;

using Enums;
using Exceptions;

public record Membership:ValueObject
{
    private readonly SubscriptionEnum _membershipType;
    private readonly DateOnly _endDate;
    private readonly DateOnly _startDate;


    private Membership(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
        {
            throw new MembershipException("End Date Cannot Be Lower Than Current Date");
        }
        _startDate = startDate;
        _endDate = endDate;
        _membershipType = SubscriptionEnum.Custom;
    }

    private Membership(SubscriptionEnum membershipType)
    {
        DateTime now = DateTime.Now;
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