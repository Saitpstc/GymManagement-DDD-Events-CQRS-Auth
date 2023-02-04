namespace Customer.Core.ValueObjects;

using Enums;
using Exceptions;

internal record Membership:ValueObject
{
    private readonly SubscriptionEnum _membershipType;
    private readonly DateTime _endDate;
    private readonly DateTime _startDate;


    private Membership(DateTime startDate, DateTime endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
        _membershipType = SubscriptionEnum.Custom;
    }

    private Membership(SubscriptionEnum membershipType)
    {
        DateTime now = DateTime.Now;
        _startDate = now;
        _endDate = now.AddMonths((int) membershipType);
        _membershipType = membershipType;
    }

    public DateTime StartedAt()
    {
        return _startDate.Date;
    }

    public SubscriptionEnum SubscriptionType() => _membershipType;

    public DateTime EndsAt()
    {
        return _endDate.Date;
    }

    public int TimePeriodInMonths()
    {
        var result = _endDate.Subtract(_startDate);
        double totalMonths = Math.Round(result.TotalDays / 30.44);
        return (int) totalMonths;
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

    public static Membership Custom(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new DomainValidationException("End Date Cannot Be Lower Than Current Date");
        }
        return new Membership(startDate, endDate);
    }
}