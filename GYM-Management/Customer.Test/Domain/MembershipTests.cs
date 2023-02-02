namespace Customer.Test.Domain;

using Core.Customer.Exceptions;
using Core.Customer.ValueObjects;
using FluentAssertions;

public class MembershipTests
{


    [Fact]
    public void If_StartDate_Is_Later_Than_EndDate_Throws_Exception()
    {
        Action action = () => Membership.Custom(DateTime.Now.AddDays(1), DateTime.Now);

        action.Should()
              .Throw<DomainValidationException>();
    }
    

    [Fact]
    public void Times_Should_Only_Contain_Date()
    {
        Membership membership = Membership.Monthly();


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
        Membership membership = Membership.Monthly();

        var mont1 = membership.EndsAt().Month;
        var mont2 = membership.StartedAt().Month;

        var differenceInMonths = mont1 - mont2;

        differenceInMonths.Should()
                          .Be(membership.TimePeriodInMonths());
    }



}