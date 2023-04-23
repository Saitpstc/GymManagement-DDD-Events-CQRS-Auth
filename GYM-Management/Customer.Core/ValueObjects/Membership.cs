namespace Customer.Core;

using Enums;
using Shared.Core.Domain;
using Shared.Core.Exceptions;

record Membership:ValueObject
{



    private Membership(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
        Status = new Status(MembershipStatus.Active, "New membership created at");
        AvailableFreezeDays = (EndDate - StartDate).Days / 4;
    }

    public int AvailableFreezeDays { get; private set; }

    public DateTime EndDate { get; }
    public DateTime StartDate { get; }

    public Status Status { get; private set; }


    public static Membership CreateNew(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new DomainValidationException("End Date Cannot Be Lower Than Current Date");
        }
        return new Membership(startDate, endDate);
    }

    public Membership Extend(DateTime endDate)
    {
        return new Membership(StartDate, endDate);
    }



    public int TimePeriodInMonths()
    {

        TimeSpan result = EndDate.Subtract(StartDate);
        var totalMonths = Math.Round(result.TotalDays / 30.44);
        return (int) totalMonths;
    }

    public void FreezeFor(int freezePeriodAsked)
    {
        var totalMembershipInDays = (EndDate - StartDate).Days;
        var maximumPossible = totalMembershipInDays / 4;

        if (freezePeriodAsked > maximumPossible)
        {
            throw new DomainValidationException(
                $"Cannot Freeze Membership More Than {maximumPossible} Days");
        }

        if (freezePeriodAsked > AvailableFreezeDays)
        {
            throw new DomainValidationException(
                $"Customer's Does Not Have Available Days to Freeze Membership;  Available Days Are {AvailableFreezeDays}");
        }
        Status = new Status(MembershipStatus.Frozen, $"Customer asked for freeze period on {DateTime.Now.Date}");
        AvailableFreezeDays -= freezePeriodAsked;
    }
}