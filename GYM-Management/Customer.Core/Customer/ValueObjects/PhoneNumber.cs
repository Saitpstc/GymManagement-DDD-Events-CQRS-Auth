namespace Customer.Core.Customer.ValueObjects;

using System.Text.RegularExpressions;
using Exceptions;
using ServiceExtensions;

internal class PhoneNumber
{
    private readonly string Number;
    private readonly string CountryCode;

    public PhoneNumber(int countryCode, string number)
    {

        if (!ValidateNumber(number))
        {
            throw new DomainValidationException($"{number} Is Not Valid");
        }
        Number = number;

        if (!ValidateCountryCode(countryCode))
        {
            throw new DomainValidationException($"{countryCode} Is Not Valid");
        }
        CountryCode = "+" + countryCode;

    }

    private bool ValidateNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || (number.Length > 10))
        {
            return false;
        }

        Regex PhoneNumberPattern = new Regex(@"^\d{10}$");

        return PhoneNumberPattern.IsMatch(number);
    }

    private bool ValidateCountryCode(int code)
    {
        return code != 0 && Service.ValidateCountryCode("+" + code);
    }

    public override string ToString() => CountryCode + Number;
}