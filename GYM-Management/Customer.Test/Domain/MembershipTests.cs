namespace Customer.Test.Domain;

using Core.Enums;
using Core.Exceptions;
using Core.ValueObjects;
using FluentAssertions;

public class MembershipTests
{


    [Fact]
    public void If_StartDate_Is_Later_Than_EndDate_Throws_Exception()
    {
        Action action = () => Membership.Custom(DateTime.Now.AddDays(1), DateTime.Now,Guid.NewGuid());

        action.Should()
              .Throw<DomainValidationException>();
    }


    [Fact]
    public void Times_Should_Only_Contain_Date()
    {
        Membership membership = Membership.Monthly(Guid.NewGuid());


        membership.StartedAt()
                  .Should()
                  .Be(DateTime.Now.Date);

        membership.EndsAt()
                  .Should()
                  .Be(DateTime.Now.AddMonths(1).Date);
    }



    [Fact]
    public void Difference_Between_StartDate_And_EndDate_Should_Be_Exactly_Described_In_SubscriptionType()
    {
        Membership membership = Membership.Monthly(Guid.NewGuid());

        var mont1 = membership.EndsAt().Month;
        var mont2 = membership.StartedAt().Month;

        var differenceInMonths = mont1 - mont2;

        differenceInMonths.Should()
                          .Be(membership.TimePeriodInMonths());
    }

    [Fact]
    public void Customer_Can_Freeze_Account_Only_For_4in1_Days_In_Total()
    {
        var mem = Membership.Yearly(Guid.NewGuid());

        Action act = (() => mem.FreezeFor(100));

        act.Should().Throw<DomainValidationException>()
           .WithMessage(
               $"Cannot Freeze Membership More Than {((mem.EndsAt() - mem.StartedAt()).Days) / 4} Days For {mem.SubscriptionType().ToString()} Subscriptions ");
    }

    [Fact]
    public void Customer_Membership_Status_Will_Be_Frozen_If_Constraints_Are_Met()
    {
        var mem = Membership.Yearly(Guid.NewGuid());

        mem.FreezeFor(80);

        Action act = () => mem.FreezeFor(20);

        mem.Status().Should().Be(MembershipStatus.Frozen);
    }

    [Fact]
    public void Customer_Membership_Status_Cannot_Be_Frozen_If_No_Days_Left()
    {
        var mem = Membership.Yearly(Guid.NewGuid());
        mem.FreezeFor(80);

        Action act = () => mem.FreezeFor(20);

        act.Should().Throw<DomainValidationException>()
           .WithMessage($"Customer's Does Not Have Available Days to Freeze Membership;  Available Days Are {mem.AvailableDaysToFreezeMembership()}");
    }



}