namespace Ledger.Core;

public interface IMembershipPriceProvider
{

    double GetMembershipPrice(int totalMembershipPeriodInMonths);
}