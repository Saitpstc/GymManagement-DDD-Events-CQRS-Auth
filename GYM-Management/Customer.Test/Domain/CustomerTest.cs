namespace Customer.Test.Domain;

using Core;
using Core.ValueObjects;
using FluentAssertions;

public class CustomerTest
{


    [Fact]
    public void CustomerShouldNotBeNull()
    {
        Customer cus = new Customer(new Name("sait", "postaic"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com"), Guid.NewGuid());

        cus.Should().NotBeNull();
    }

    [Fact]
    public void IncrementTotalMonthsOfMembership_When_NewMembershipStarted()
    {
        Customer cus = new Customer(new Name("sait", "postaic"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com"), Guid.NewGuid());

        cus.StartMembership(Membership.CreateNew(DateTime.Now, DateTime.Now.AddMonths(6)));

        cus.TotalMonthsOfMembership.Should().Be(6);


    }

    [Fact]
    public void IncrementTotalMontshOfMembership_When_MembershipExtended()
    {
        Customer cus = new Customer(new Name("sait", "postaic"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com"), Guid.NewGuid());

        cus.StartMembership(Membership.CreateNew(DateTime.Now, DateTime.Now.AddMonths(6)));

        cus.TotalMonthsOfMembership.Should().Be(6);

        cus.ExtendMembership(DateTime.Now.AddMonths(7));
    }

    [Fact]
    public void MembershipShouldBeNull_When_Terminated()
    {
        Customer cus = new Customer(new Name("sait", "postaic"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com"), Guid.NewGuid());

        cus.StartMembership(Membership.CreateNew(DateTime.Now, DateTime.Now.AddMonths(6)));

        cus.TerminateMembership();

        cus.Membership.Should().BeNull();
    }

}