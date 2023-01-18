namespace Shared.Test.ValueObjectTests;

using Core;

public record Money(double Amount, string Currency) : ValueObject
{
    
    public static Money CreateInstance(double Amount, string Currency)
    {
        return new Money(Amount, Currency);
    }

    public string Currency { get; init; } = Currency ?? throw new ArgumentNullException(nameof(Currency));
    public double Amount { get; init; } = Amount == 0 ? throw new ArgumentNullException(nameof(Amount)) : Amount;

    /*public void Deconstruct(out double Amount, out string Currency)
    {
        Amount = this.Amount;
        Currency = this.Currency;
    }*/
};