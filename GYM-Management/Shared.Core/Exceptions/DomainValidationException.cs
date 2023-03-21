namespace Shared.Core.Exceptions;

public class DomainValidationException:BaseException
{
    public DomainValidationException(string message):base(message)
    {
        ErrorMessages.Add(message);

    }
}