namespace Customer.Core.ValueObjects;

using System.Text.RegularExpressions;
using Exceptions;

public record Name:ValueObject
{
    private readonly string _name;
    private readonly string _surname;

    public Name(string name, string surname)
    {
        InputContainsOnlyLetters(name, surname);
        _name = name[..1].ToUpper() + name[1..];
        _surname = surname[..1].ToUpper() + surname[1..];

    }

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
        return _name + " " + _surname;
    }

    public string NameOnly() => _name;
    public string SurNameOnly() => _surname;

    public string NormalizedName() => _name.ToUpper();
    public string NormalizedSurName() => _surname.ToUpper();
}