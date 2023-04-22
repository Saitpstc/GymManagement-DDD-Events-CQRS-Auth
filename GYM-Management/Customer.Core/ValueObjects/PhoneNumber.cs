namespace Customer.Core.ValueObjects;

using System.Text.RegularExpressions;
using ServiceExtensions;
using Shared.Core.Exceptions;

class PhoneNumber
{

    protected PhoneNumber()
    {

    }

    public PhoneNumber(string countryCountryCode, string number)
    {

        if (!ValidateNumber(number))
        {
            throw new DomainValidationException($"Phone Number:{number} Is Not Valid");
        }
        Number = number;

        if (!ValidateCountryCode(countryCountryCode))
        {
            throw new DomainValidationException($"Country CountryCodeOnly:{countryCountryCode} Is Not Valid");
        }

        CountryCode = countryCountryCode;

    }

    public string Number { get; }
    public string CountryCode { get; }

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
        return "+" + CountryCode + Number;
    }

    public string CountryCodeOnly()
    {
        return CountryCode;
    }

    public string NumberOnly()
    {
        return Number;
    }
}