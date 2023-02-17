namespace Customer.Core.ValueObjects;

using System.Text.RegularExpressions;
using Exceptions;
using ServiceExtensions;

public record Email:ValueObject
{
    private string email;

    public Email(string email)
    {
        ValidateEmail(email);
        this.email = email;
    }

    static private readonly Regex EmailPattern = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

    private void ValidateEmail(string email)
    {
        Service.CheckIfStringValid(email, "Email Is Not Valid");

        if (!EmailPattern.IsMatch(email))
        {
            throw new DomainValidationException("Email Is Invalid");
        }

    }

    public override string ToString() => email;
}