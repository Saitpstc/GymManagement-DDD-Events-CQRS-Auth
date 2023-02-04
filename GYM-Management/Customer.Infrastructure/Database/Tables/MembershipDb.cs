namespace Customer.Infrastructure.Database.Tables;

using Core.Enums;
using Shared.Core;

internal class MembershipDb
{
    public SubscriptionEnum SubscriptionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}