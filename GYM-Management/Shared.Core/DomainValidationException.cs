namespace Shared.Core;

public class DomainValidationException:Exception
{
    public DomainValidationException(string message):base(message)
    {
        
    }    
}