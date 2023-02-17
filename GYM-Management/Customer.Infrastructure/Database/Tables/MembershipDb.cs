namespace Customer.Infrastructure.Database.Tables;

using Core.Enums;
using Shared.Core;

public class MembershipDb
{
    public SubscriptionEnum SubscriptionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MembershipStatus Status { get; set; }
    public int AvailableFreezePeriod { get; set; }
    public int TotalMonthsOfMembership { get; set; }
}