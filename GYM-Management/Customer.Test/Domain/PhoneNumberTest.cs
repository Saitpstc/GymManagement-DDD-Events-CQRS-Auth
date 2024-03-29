﻿namespace Customer.Test.Domain;

using Core.ServiceExtensions;
using Core.ValueObjects;
using FluentAssertions;
using Shared.Core.Exceptions;

public class PhoneNumberTest
{
    [Fact]
    public void CountryCode_Should_Be_Validated()
    {
        var turkeyCode = "+90";

        var validation = Service.ValidateCountryCode(turkeyCode);

        validation.Should().Be(true);

    }

    [Fact]
    public void If_Country_Code_Invalid_It_Should_Throw_Exception()
    {
        Action InvalidAction = () => new PhoneNumber("92220", "5342564577");

        Action ValidAction = () => new PhoneNumber("90", "5342564577");


        InvalidAction.Should()
                     .Throw<DomainValidationException>();

        ValidAction.Should().NotThrow<DomainValidationException>();
    }

    [Fact]
    public void Number_Cannot_Be_More_Than_10_Char()
    {
        Action action = () => new PhoneNumber("90", "53425645632");
        action.Should()
              .Throw<DomainValidationException>();
    }

    [Fact]
    public void Phone_Number_Should_Be_Returned_With_Country_Code()
    {
        PhoneNumber number = new PhoneNumber("90", "5533282568");

        var fullNumber = number.ToString();

        fullNumber.Should().Be("+905533282568");
    }

    [Fact]
    public void CountryCode_Cannot_Be_Zero_And_Number_Cannot_Be_Empty_Or_Null_Or_WhiteSpace()
    {
        Action EmptyNumber = () => new PhoneNumber("90", "");
        Action ZeroCodeAndEmptyNumber = () => new PhoneNumber("0", "");
        Action NullNumber = () => new PhoneNumber("90", null);
        Action WhiteSpaceNumber = () => new PhoneNumber("90", " ");


        EmptyNumber.Should().Throw<DomainValidationException>();
        ZeroCodeAndEmptyNumber.Should().Throw<DomainValidationException>();
        NullNumber.Should().Throw<DomainValidationException>();
        WhiteSpaceNumber.Should().Throw<DomainValidationException>();

    }
}