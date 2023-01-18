namespace Shared.Test.ValueObjectTests;

using Core;

public class ValueObjectUnitTest
{
    [Fact]
    public void Test_Passes_If_Two_ValueObject_Is_Equal_In_Terms_Of_Values()
    {
        //Arrange
        ValueObject tenDollars = new Money(11, "USD");

        ValueObject elevenDollars = new Money(11, "USD");

        //Act
        bool EqualityCheck = tenDollars.Equals(elevenDollars);

        bool EqualityOperatorResult = tenDollars == elevenDollars;

        //Assert
        EqualityCheck.Should()
                     .Be(true);

        EqualityOperatorResult.Should()
                              .Be(true);
    }

    [Fact]
    public void Test_Fails_If_Two_ValueObject_Is_Not_Equal_In_Terms_Of_Values()
    {
        //Arrange
        ValueObject tenDollars = new Money(10, "USD");

        ValueObject elevenDollars = new Money(11, "USD");

        //Act
        bool EqualityCheck = tenDollars.Equals(elevenDollars);

        bool EqualityOperatorResult = tenDollars != elevenDollars;


        //Assert
        EqualityCheck.Should()
                     .Be(false);

        EqualityOperatorResult.Should()
                              .Be(true);
    }

    [Fact]
    public void Test_Passes_If_Same_ValueObjects_Has_Same_HashCode()
    {
        //Arrange
        ValueObject tenDollars = new Money(11, "USD");

        ValueObject elevenDollars = new Money(11, "USD");


        //Act
        int TenDollarsHashCode = tenDollars.GetHashCode();
        int ElevenDollarsHashCode = elevenDollars.GetHashCode();

        //Assert
        TenDollarsHashCode.Should()
                          .Be(ElevenDollarsHashCode);
    }

    [Fact]
    public void Test_Passes_If_ToString_Method_Returns_Expected_Result()
    {
        //Arrange
        ValueObject tenDollars = new Money(11, "USD");
        string ExpectedString = "Money { Currency = USD, Amount = 11 }";

        //Act
        bool EqualityCondition = tenDollars.ToString()
                                           .Equals(ExpectedString);

        //Assert

        EqualityCondition.Should()
                         .Be(true);
    }

    [Fact]
    public void Test_Passes_If_ValueObject_Deconstruct_Correctly()
    {
        //Arrange
        Money tenDollars = new Money(11, "USD");
        double Amount;
        string Currency;

        //Act
        tenDollars.Deconstruct(Amount: out Amount, out Currency);


        //Assert

        Amount.Should()
              .Be(11);
        Currency.Should()
                .Be("USD");
    }
}