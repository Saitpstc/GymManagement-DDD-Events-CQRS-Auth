namespace Customer.Test.Domain;

using Core;
using Core.Enums;
using FluentAssertions;
using Shared.Core.Exceptions;

public class MembershipTests
{


    [Fact]
    public void If_StartDate_Is_Later_Than_EndDate_Throws_Exception()
    {
        Action action = () => Membership.CreateNew(DateTime.Now.AddDays(1), DateTime.Now);

        action.Should()
              .Throw<DomainValidationException>();
    }

    [Fact]
    public void If_StartDate_Is_Before_Than_EndDate_It_Should_Return_New_Membership()
    {
        Membership mem = Membership.CreateNew(DateTime.Now, DateTime.Now.AddDays(1));

        mem.Should()
           .NotBeNull();
    }




    [Fact]
    public void Customer_Can_Freeze_Account_Only_For_4in1_Days_In_Total()
    {
        Membership mem = Membership.CreateNew(DateTime.Now, DateTime.Now.AddDays(360));

        Action act = () => mem.FreezeFor(100);

        act.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Customer_Membership_Status_Will_Be_Frozen_If_Constraints_Are_Met()
    {
        Membership mem = Membership.CreateNew(DateTime.Now, DateTime.Now.AddDays(2000));

        mem.FreezeFor(80);

        Action act = () => mem.FreezeFor(20);

        mem.Status.Should().Be(MembershipStatus.Frozen);
    }

    [Fact]
    public void Customer_Membership_Status_Cannot_Be_Frozen_If_No_Days_Left()
    {
        Membership mem = Membership.CreateNew(DateTime.Now, DateTime.Now.AddDays(360));
        mem.FreezeFor(90);

        Action act = () => mem.FreezeFor(20);

        act.Should().Throw<DomainValidationException>();
    }
}