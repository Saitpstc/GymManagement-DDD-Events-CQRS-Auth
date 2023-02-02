namespace Customer.Test.Domain;

using Core.Customer.Exceptions;
using Core.Customer.ValueObjects;
using FluentAssertions;

public class NameTest
{
    [Fact]
    public void Name_First_Letter_Should_Start_With_UpperCase()
    {
        var name = "Sait";
        var surnName = "Sait";
        Name fullName = new Name(name, surnName);

        var result = fullName.OfCustomer();

     
        result.Should().Be("Sait Sait");
    }

    [Fact]
    public void Name_Should_Accept_Only_Letters()
    {
        Action fullName =()=> new Name("sa@!t", "p0st4c!");

        fullName.Should().Throw<DomainValidationException>();
    }

    [Fact]
    public void GetName_And_GetSurname_Should_Return_Only_Name_Without_Surname()
    {
        Name fullName = new Name("sait","postaci");

        var nameOnly=fullName.NameOnly();
        var surnameOnly=fullName.SurNameOnly();

        nameOnly.Should().Be("Sait");
        surnameOnly.Should().Be("Postaci");
    }

    [Fact]
    public void Normalized_Versions_Of_Name_And_Surname()
    {
        Name fullName = new Name("sait","postaci");

        var NormalizedName = fullName.NormalizedName();
        var NormalizedSurName = fullName.NormalizedSurName();

        NormalizedName.Should().Be("SAIT");
        NormalizedSurName.Should().Be("POSTACI");
    }

}