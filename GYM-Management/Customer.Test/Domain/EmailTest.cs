namespace Customer.Test.Domain;

using Core.Exceptions;
using Core.ValueObjects;
using FluentAssertions;

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
        Action action = () => new Email("saitpostaci#gmail.com");

        action.Should().Throw<DomainValidationException>();
    }

}