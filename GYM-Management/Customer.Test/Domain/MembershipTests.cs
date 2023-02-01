namespace Customer.Test.Domain;

using Core.Customer.Exceptions;
using Core.Customer.ValueObjects;
using FluentAssertions;

public class MembershipTests
{


    [Fact]
    public void If_StartDate_Is_Later_Than_EndDate_Throws_Exception()
    {
        Action action = () => Membership.Custom(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now));

        action.Should()
              .Throw<MembershipException>();
    }

    [Fact]
    public void Times_Should_Only_Contain_Date()
    {
        Membership membership = Membership.Monthly();


        membership.StartedAt()
                  .Should()
                  .Be(DateOnly.FromDateTime(DateTime.Now));

        membership.EndsAt()
                  .Should()
                  .Be(DateOnly.FromDateTime(DateTime.Now.AddMonths(1)));
    }

    [Fact]
    public void If_StartDate_Is_Lower_Than_CurrentDate_Throws_Exception()
    {
        Action action = () => Membership.Custom(DateOnly.MinValue, DateOnly.FromDateTime(DateTime.Now));

        action.Should()
              .Throw<MembershipException>().WithMessage("Start Date Cannot Be Lower Than Current Date");
    }

    [Fact]
    public void Difference_Between_StartDate_And_EndDate_Should_Be_Exactly_Described_In_SubscriptionType()
    {
        Membership membership = Membership.Monthly();

        var mont1 = membership.EndsAt().Month;
        var mont2 = membership.StartedAt().Month;

        var differenceInMonths = mont1 - mont2;

        differenceInMonths.Should()
                          .Be(membership.TimePeriod());
    }

    [Fact]
    public void Owner_Can_Create_Membership_Without_SubscriptionType_Just_Dates()
    {
        Membership membership = Membership.Custom(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(1)));

    }

    [Fact]
    public void If_Membership_Created_Through_Custom_Period_Then_TimePeriod_Should_Be_Zero()
    {
        Membership membership = Membership.Custom(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(1)));

        membership.TimePeriod().Should().Be(0);

    }
}