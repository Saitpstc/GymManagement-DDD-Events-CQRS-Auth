namespace Customer.Test.Domain;

using Core.Enums;
using Core.ValueObjects;
using FluentAssertions;
using Shared.Core;
using Shared.Core.Domain;
using Shared.Core.Exceptions;

public class MembershipTests
{


    [Fact]
    public void If_StartDate_Is_Later_Than_EndDate_Throws_Exception()
    {
        Action action = () => Membership.Custom(DateTime.Now.AddDays(1), DateTime.Now, Guid.NewGuid());

        action.Should()
              .Throw<DomainValidationException>();
    }

    [Fact]
    public void If_StartDate_Is_Before_Than_EndDate_It_Should_Return_New_Membership()
    {
        Membership mem = Membership.Custom(DateTime.Now, DateTime.Now.AddDays(1), Guid.NewGuid());

        mem.Should()
           .NotBeNull();
    }


    [Fact]
    public void Times_Should_Only_Contain_Date()
    {
        Membership membership = Membership.Custom(DateTime.Now, DateTime.Now.AddMonths(1), Guid.NewGuid());


        membership.StartDate
                  .Should()
                  .Be(DateTime.Now.Date);

        membership.EndDate
                  .Should()
                  .Be(DateTime.Now.AddMonths(1).Date);
    }




    [Fact]
    public void Customer_Can_Freeze_Account_Only_For_4in1_Days_In_Total()
    {
        Membership mem = Membership.Custom(DateTime.Now, DateTime.Now.AddDays(360), Guid.NewGuid());

        Action act = () => mem.FreezeFor(100);

        act.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Customer_Membership_Status_Will_Be_Frozen_If_Constraints_Are_Met()
    {
        Membership mem = Membership.Custom(DateTime.Now, DateTime.Now.AddDays(2000), Guid.NewGuid());

        mem.FreezeFor(80);

        Action act = () => mem.FreezeFor(20);

        mem.Status.Should().Be(MembershipStatus.Frozen);
    }

    [Fact]
    public void Customer_Membership_Status_Cannot_Be_Frozen_If_No_Days_Left()
    {
        Membership mem = Membership.Custom(DateTime.Now, DateTime.Now.AddDays(360), Guid.NewGuid());
        mem.FreezeFor(90);

        Action act = () => mem.FreezeFor(20);

        act.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void Total_Membership_In_Months()
    {

        Guid guid = Guid.NewGuid();
        Membership mem = Membership.Custom(DateTime.Now, DateTime.Now.AddMonths(12), Guid.NewGuid());

        mem.GetTotalMembershipInMonths().Should().Be(12);


        mem.RenewMembershipPeriod(guid, DateTime.Now, DateTime.Now.AddMonths(1));

        mem.GetTotalMembershipInMonths().Should().Be(13);

        mem.RenewMembershipPeriod(guid, DateTime.Now, DateTime.Now.AddMonths(12));

        mem.GetTotalMembershipInMonths().Should().Be(25);
    }
}