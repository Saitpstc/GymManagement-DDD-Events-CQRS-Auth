namespace Customer.Test.Domain;

using Core.ValueObjects;
using FluentAssertions;
using Shared.Core;

public class EmailTest
{
    [Fact]
    public void Email_Should_Pass_ValidationRegex()
    {
        Action action = () => new Email("saitpostaci#gmail.com");

        
        action.Should().Throw<DomainValidationException>().WithMessage("Email Is Invalid");
    }

    [Fact]
    public void Email_Cannot_Be_Null_Or_Empty_Or_WhiteSpace()
    {
        Action action = () => new Email("");

        action.Should().Throw<DomainValidationException>();
    }
    [Fact]
    public void Email_Should_Pass_Validation()
    {
        Action action = () => new Email("saitpostaci@gmail.com");

        action.Should().NotThrow();
    }
    [Fact]
    public void ToString_Should_Return_Email()
    {
        Email mail= new Email("saitpostaci@gmail.com");

        mail.ToString().Should().Be("saitpostaci@gmail.com");
    }

}