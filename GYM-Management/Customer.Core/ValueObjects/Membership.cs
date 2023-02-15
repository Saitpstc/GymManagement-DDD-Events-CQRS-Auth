namespace Customer.Core.ValueObjects;

using Enums;
using Exceptions;

internal class Membership:BaseEntity
{

    public Guid CustomerId { get; }
    private SubscriptionEnum _subscriptionType;
    private DateTime _endDate;
    private DateTime _startDate;
    private MembershipStatus _status;
    private int _availableFreezePeriod;


    private Membership(DateTime startDate, DateTime endDate, Guid customerId)
    {
        _startDate = startDate;
        _endDate = endDate;
        CustomerId = customerId;
        _subscriptionType = SubscriptionEnum.Custom;
        _status = MembershipStatus.Active;
        _availableFreezePeriod = ((endDate - startDate).Days) / 4;
    }

    private Membership(SubscriptionEnum subscriptionType,Guid customerId)
    {
        CustomerId = customerId;
        DateTime now = DateTime.Now;
        _startDate = now;
        _endDate = now.AddMonths((int) subscriptionType);
        _subscriptionType = subscriptionType;
        _status = MembershipStatus.Active;
        _availableFreezePeriod = ((_endDate - _startDate).Days) / 4;
    }

    public DateTime StartedAt()
    {
        return _startDate.Date;
    }

    public SubscriptionEnum SubscriptionType() => _subscriptionType;

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


    public static Membership Monthly(Guid customerId)
    {
        return new Membership(SubscriptionEnum.Monthly,customerId);
    }

    public static Membership Quarterly(Guid customerId)
    {
        return new Membership(SubscriptionEnum.Quarterly,customerId);
    }

    public static Membership HalfYear(Guid customerId)
    {
        return new Membership(SubscriptionEnum.HalfYear,customerId);
    }

    public static Membership Yearly(Guid customerId)
    {
        return new Membership(SubscriptionEnum.Yearly,customerId);
    }

    public static Membership Custom(DateTime startDate, DateTime endDate,Guid customerId)
    {
        if (startDate > endDate)
        {
            throw new DomainValidationException("End Date Cannot Be Lower Than Current Date");
        }
        return new Membership(startDate, endDate,customerId);
    }

    public void FreezeFor(int freezePeriodAsked)
    {
        var totalMembershipInDays = (_endDate - _startDate).Days;
        var maximumPossible = (totalMembershipInDays / 4);

        if (freezePeriodAsked > maximumPossible)
        {
            throw new DomainValidationException(
                $"Cannot Freeze Membership More Than {maximumPossible} Days For {_subscriptionType.ToString()} Subscriptions ");
        }

        if (freezePeriodAsked > _availableFreezePeriod)
        {
            throw new DomainValidationException(
                $"Customer's Does Not Have Available Days to Freeze Membership;  Available Days Are {_availableFreezePeriod}");
        }
        _status = MembershipStatus.Frozen;
        _availableFreezePeriod -= freezePeriodAsked;
    }

    public MembershipStatus Status() => _status;

    public int AvailableDaysToFreezeMembership() => _availableFreezePeriod;
}