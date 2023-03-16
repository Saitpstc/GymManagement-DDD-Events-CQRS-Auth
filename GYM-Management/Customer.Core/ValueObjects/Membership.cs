namespace Customer.Core.ValueObjects;

using Enums;
using Shared.Core.Domain;

public class Membership:BaseEntity
{
    private int _availableFreezePeriod;
    private DateTime _endDate;
    private DateTime _startDate;
    private MembershipStatus _status;
    private SubscriptionEnum _subscriptionType;
    private int _totalMonthsOfMembership;

    public Guid CustomerId { get; set; }

    private void ValidateMembership(SubscriptionEnum type, Guid customerId, DateTime? startDate = null, DateTime? endDate = null)
    {

        if (type == SubscriptionEnum.Custom)
        {
            CustomTypeChecks(startDate, endDate);
        }
        else
        {
            StaticTypeChecks(type);
        }

        _status = MembershipStatus.Active;
        _availableFreezePeriod = (_endDate - _startDate).Days / 4;

        CustomerId = customerId;
    }

    private void StaticTypeChecks(SubscriptionEnum type)
    {

        if (_status == MembershipStatus.DeActive)
        {
            _startDate = DateTime.Now;
            _endDate = _startDate.AddMonths((int) type);
            _subscriptionType = type;
            _totalMonthsOfMembership += TimePeriodInMonths();
        }
        else
        {
            _endDate = _endDate.AddMonths((int) type);
            _subscriptionType = SubscriptionEnum.Custom;
            _totalMonthsOfMembership += (int) type;
        }
    }

    private void CustomTypeChecks(DateTime? startDate, DateTime? endDate)
    {
        if (startDate is null || endDate is null)
        {
            throw new DomainValidationException("Custom Memberships Should Provide StartDate and EndDate");
        }

        if (_status == MembershipStatus.DeActive)
        {
            _startDate = (DateTime) startDate;
            _endDate = (DateTime) endDate;
            _totalMonthsOfMembership += TimePeriodInMonths();
        }
        else
        {
            var totalMonths = ((DateTime) endDate).Subtract((DateTime) startDate).TotalDays / 30.44;
            var integerValueOfMonths = (int) Math.Round(totalMonths);
            _endDate = _endDate.AddMonths(integerValueOfMonths);
            _totalMonthsOfMembership += integerValueOfMonths;
        }

        _subscriptionType = SubscriptionEnum.Custom;
    }


    #region ConstructorAndFactories

    private Membership(SubscriptionEnum type, Guid customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        ValidateMembership(type, customerId, startDate, endDate);
    }

    public static Membership CreateMembershipPeriodOf(SubscriptionEnum type, Guid customerId)
    {
        return new Membership(type, customerId);
    }

    public void RenewMembershipPeriod(SubscriptionEnum type, Guid customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        ValidateMembership(type, customerId, startDate, endDate);
    }

    public static Membership Custom(DateTime startDate, DateTime endDate, Guid customerId)
    {
        if (startDate > endDate)
        {
            throw new DomainValidationException("End Date Cannot Be Lower Than Current Date");
        }
        return new Membership(SubscriptionEnum.Custom, customerId, startDate, endDate);
    }

    #endregion

    #region PublicDataProviders

    public MembershipStatus Status()
    {
        return _status;
    }

    public int AvailableDaysToFreezeMembership()
    {
        return _availableFreezePeriod;
    }

    public int GetTotalMembershipInMonths()
    {
        return _totalMonthsOfMembership;
    }

    public DateTime StartedAt()
    {
        return _startDate.Date;
    }

    public SubscriptionEnum SubscriptionType()
    {
        return _subscriptionType;
    }

    public DateTime EndsAt()
    {
        return _endDate.Date;
    }

    public int TimePeriodInMonths()
    {
        TimeSpan result = _endDate.Subtract(_startDate);
        var totalMonths = Math.Round(result.TotalDays / 30.44);
        return (int) totalMonths;
    }

    #endregion

    #region Commands

    public void FreezeFor(int freezePeriodAsked)
    {
        var totalMembershipInDays = (_endDate - _startDate).Days;
        var maximumPossible = totalMembershipInDays / 4;

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

    public void TerminateMembership()
    {
        _status = MembershipStatus.DeActive;
    }

    #endregion
}