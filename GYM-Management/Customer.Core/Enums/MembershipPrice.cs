namespace Customer.Core.Enums;

using System.ComponentModel;

public enum MembershipPrice
{
    [Description("300")]
    Monthly,
    [Description("800")]
    Quarterly,
    [Description("1500")]
    HalfYear,
    [Description("2800")]
    Yearly
}