namespace Customer.Core.Exceptions;

internal class DomainValidationException:Exception
{
    public DomainValidationException(string message):base(message)
    {
        
    }    
}