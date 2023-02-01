namespace Customer.Core.Customer.Exceptions;

public class CustomerDomainException:Exception
{
    public CustomerDomainException(string Message, Type type):base(Message)
    {

    }
}