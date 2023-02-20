namespace Customer.Infrastructure.Database.Tables;

using Core.Enums;
using Shared.Core;

public class MembershipDb:BaseEntity
{
    public SubscriptionEnum SubscriptionType { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerDB Customer { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MembershipStatus Status { get; set; }
    public int AvailableFreezePeriod { get; set; }
    public int TotalMonthsOfMembership { get; set; }
}