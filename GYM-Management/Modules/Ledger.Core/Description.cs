namespace Ledger.Test;

using Shared.Core.Domain;
using Shared.Core.Exceptions;

public record Description:ValueObject
{
    public Description(string description)
    {
        if (description.Length > 255)
        {
            throw new DomainValidationException("Description cannot be more than 255 character");
        }
        Value = description;
    }

    public string Value { get; }
}