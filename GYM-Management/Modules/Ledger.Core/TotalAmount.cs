namespace Ledger.Test;

using Shared.Core.Exceptions;

public record TotalAmount
{
    public TotalAmount(double amount)
    {
        if (amount < 0)
        {
            throw new DomainValidationException("An exception has been thrown");
        }
        this.amount = amount;

    }

    public double amount { get; }


}