namespace Customer.Core.ValueObjects;

using System.Text.RegularExpressions;
using Shared.Core.Domain;
using Shared.Core.Exceptions;

record Name:ValueObject
{

    protected Name()
    {

    }

    public Name(string name, string surname)
    {
        InputContainsOnlyLetters(name, surname);
        FirstName = name[..1].ToUpper() + name[1..];
        Surname = surname[..1].ToUpper() + surname[1..];

    }

    public string FirstName { get; }
    public string Surname { get; }

    private void InputContainsOnlyLetters(string name, string surname)
    {
        Regex rule = new Regex("^[a-zA-Z]+$");

        if (!rule.IsMatch(name))
        {
            throw new DomainValidationException("Name Should Only Contain Letters");
        }

        if (!rule.IsMatch(surname))
        {
            throw new DomainValidationException("Surname Should Only Contain Letters1");
        }
    }

    public string OfCustomer()
    {
        return FirstName + " " + Surname;
    }

    public string NameOnly()
    {
        return FirstName;
    }

    public string SurNameOnly()
    {
        return Surname;
    }

    public string NormalizedName()
    {
        return FirstName.ToUpper();
    }

    public string NormalizedSurName()
    {
        return Surname.ToUpper();
    }
}