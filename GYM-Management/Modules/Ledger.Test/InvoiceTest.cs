namespace Ledger.Test;

using FluentAssertions;
using Shared.Core.Exceptions;

public class InvoiceTest
{
    [Fact]
    public void Throws_If_TotalAmount_Is_LowerThanZero()
    {

        Action action = () =>
        {
            TotalAmount totalAmount = new TotalAmount(-6);
        };

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void NotThrows_If_TotalAmount_Is_GreaterThanZero()
    {

        Action action = () =>
        {
            TotalAmount totalAmount = new TotalAmount(6);
        };

        action.Should().NotThrow<Exception>();
    }

    [Theory]
    [InlineData(1.5)]
    [InlineData(56)]
    [InlineData(34.5673)]
    public void ReturnTotalAmount_If_Creation_Is_Sucessfull(double testValue)
    {
        TotalAmount totalAmount = new TotalAmount(testValue);


        totalAmount.amount.Should().Be(testValue);
    }

    [Fact]
    public void Throws_When_DueDate_Is_Lower_Than_DateTimeNow()
    {

        Action action = () => new Invoice(new TotalAmount(12), DateTime.Now);

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void InvoiceShouldNotBeNull_When_ParametersAreCorrect()
    {
        
        
    }
}

public class Invoice
{
    public TotalAmount TotalAmount { get; }

    public Invoice(TotalAmount totalAmount, DateTime dateTime)
    {
        throw new DomainValidationException("An exception has been thrown");
        TotalAmount = totalAmount;
    }
}

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