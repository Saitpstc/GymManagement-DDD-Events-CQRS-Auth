namespace Customer.Core.ValueObjects;

using System.Text.RegularExpressions;
using ServiceExtensions;
using Shared.Core.Domain;
using Shared.Core.Exceptions;

public class PhoneNumber
{
    private readonly string _number;
    private readonly string Code;

    public PhoneNumber(string countryCode, string number)
    {

        if (!ValidateNumber(number))
        {
            throw new DomainValidationException($"Phone _number:{number} Is Not Valid");
        }
        _number = number;

        if (!ValidateCountryCode(countryCode))
        {
            throw new DomainValidationException($"Country Code:{countryCode} Is Not Valid");
        }

        Code = countryCode;

    }

    private bool ValidateNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Length > 10)
        {
            return false;
        }

        Regex PhoneNumberPattern = new Regex(@"^\d{10}$");

        return PhoneNumberPattern.IsMatch(number);
    }

    private bool ValidateCountryCode(string code)
    {
        return !string.IsNullOrWhiteSpace(code) && Service.ValidateCountryCode("+" + code);
    }

    public override string ToString()
    {
        return "+" + Code + _number;
    }

    public string CountryCode()
    {
        return Code;
    }

    public string Number()
    {
        return _number;
    }
}