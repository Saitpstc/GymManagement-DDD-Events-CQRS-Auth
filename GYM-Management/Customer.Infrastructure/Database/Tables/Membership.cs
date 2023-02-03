namespace Customer.Infrastructure.Database.Tables;

using Core.Customer.Enums;
using Shared.Core;

public class Membership:BaseEntity
{
    public SubscriptionEnum SubscriptionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}