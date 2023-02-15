namespace Customer.Test.Domain;

using Core;
using Core.ValueObjects;
using FluentAssertions;

public class CustomerTest
{
    

    [Fact]
    public void d()
    {
        var cus = new Customer(new Name("sait", "postaic"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com"));

        var t = cus.GetMembership();
        cus.Should().NotBeNull();
    }
}