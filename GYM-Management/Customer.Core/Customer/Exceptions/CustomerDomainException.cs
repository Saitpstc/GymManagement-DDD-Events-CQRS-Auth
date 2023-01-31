namespace Customer.Core.Customer.Exceptions;

using Exception = System.Exception;

public class CustomerDomainException:Exception
{
    public CustomerDomainException(string Message , Type type) :base(Message)
    {
        
    }
}