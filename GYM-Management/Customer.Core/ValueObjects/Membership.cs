namespace Customer.Core.ValueObjects;

using Enums;
using Shared.Core.Domain;
using Shared.Core.Exceptions;

public class Membership:BaseEntity
{
    private int _availableFreezePeriod;
    public DateTime EndDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public MembershipStatus Status { get; private set; }
    private int _totalMonthsOfMembership;

    public Guid CustomerId { get; set; }




    private void ValidateMembership(Guid customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        CustomTypeChecks(startDate, endDate);
        Status = MembershipStatus.Active;
        _availableFreezePeriod = (EndDate - StartDate).Days / 4;
        CustomerId = customerId;
    }



    private void CustomTypeChecks(DateTime? startDate, DateTime? endDate)
    {
        if (startDate is null || endDate is null)
        {
            throw new DomainValidationException("Custom Memberships Should Provide StartDate and EndDate");
        }

        if (Status == MembershipStatus.DeActive)
        {
            StartDate = (DateTime) startDate;
            EndDate = (DateTime) endDate;
            _totalMonthsOfMembership += TimePeriodInMonths();
        }
        else
        {
            var totalMonths = ((DateTime) endDate).Subtract((DateTime) startDate).TotalDays / 30.44;
            var integerValueOfMonths = (int) Math.Round(totalMonths);
            EndDate = EndDate.AddMonths(integerValueOfMonths);
            _totalMonthsOfMembership += integerValueOfMonths;
        }

    }


    #region ConstructorAndFactories

    private Membership(Guid customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        ValidateMembership(customerId, startDate, endDate);
    }

    public void RenewMembershipPeriod(Guid customerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        ValidateMembership(customerId, startDate, endDate);
    }

    public static Membership Custom(DateTime startDate, DateTime endDate, Guid customerId)
    {
        if (startDate > endDate)
        {
            throw new DomainValidationException("End Date Cannot Be Lower Than Current Date");
        }
        return new Membership(customerId, startDate, endDate);
    }

    #endregion

    #region PublicDataProviders

    public int TimePeriodInMonths()
    {
        TimeSpan result = EndDate.Subtract(StartDate);
        var totalMonths = Math.Round(result.TotalDays / 30.44);
        return (int) totalMonths;
    }

    #endregion

    #region Commands

    public void FreezeFor(int freezePeriodAsked)
    {
        var totalMembershipInDays = (EndDate - StartDate).Days;
        var maximumPossible = totalMembershipInDays / 4;

        if (freezePeriodAsked > maximumPossible)
        {
            throw new DomainValidationException(
                $"Cannot Freeze Membership More Than {maximumPossible} Days");
        }

        if (freezePeriodAsked > _availableFreezePeriod)
        {
            throw new DomainValidationException(
                $"Customer's Does Not Have Available Days to Freeze Membership;  Available Days Are {_availableFreezePeriod}");
        }
        Status = MembershipStatus.Frozen;
        _availableFreezePeriod -= freezePeriodAsked;
    }

    public void TerminateMembership()
    {
        Status = MembershipStatus.DeActive;
    }

    #endregion

    public int GetTotalMembershipInMonths()
    {
        return _totalMonthsOfMembership;
    }
}