namespace Customer.Core.Customer.Exceptions;

public class DomainValidationException:Exception
{
    public DomainValidationException(string message):base(message)
    {
        
    }    
}