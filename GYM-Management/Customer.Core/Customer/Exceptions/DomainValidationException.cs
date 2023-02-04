namespace Customer.Core.Customer.Exceptions;

internal class DomainValidationException:Exception
{
    public DomainValidationException(string message):base(message)
    {
        
    }    
}