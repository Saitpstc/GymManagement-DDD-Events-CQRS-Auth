namespace Shared.Core.Domain;

public class DomainValidationException:Exception
{
    public DomainValidationException(string message):base(message)
    {

    }
}