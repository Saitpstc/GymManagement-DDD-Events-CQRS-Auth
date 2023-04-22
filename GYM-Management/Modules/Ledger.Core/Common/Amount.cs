namespace Ledger.Test;

using Shared.Core.Exceptions;

public record Amount
{
    public Amount(double amount)
    {
        if (amount < 0)
        {
            throw new DomainValidationException("Amount cannot be lower than 0");
        }
        this.amount = amount;

    }

    public double amount { get; }
}