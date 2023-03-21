namespace Customer.Core.ValueObjects;

using System.Text.RegularExpressions;
using ServiceExtensions;
using Shared.Core.Domain;
using Shared.Core.Exceptions;

public record Email:ValueObject
{

    static private readonly Regex EmailPattern = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
    private readonly string email;

    public Email(string email)
    {
        ValidateEmail(email);
        this.email = email;
    }

    private void ValidateEmail(string email)
    {
        Service.CheckIfStringValid(email, "Email Is Not Valid");

        if (!EmailPattern.IsMatch(email))
        {
            throw new DomainValidationException("Email Is Invalid");
        }

    }

    public override string ToString()
    {
        return email;
    }
}