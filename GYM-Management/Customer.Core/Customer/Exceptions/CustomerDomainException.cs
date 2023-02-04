namespace Customer.Core.Customer.Exceptions;

internal class CustomerDomainException:Exception
{
    public CustomerDomainException(string Message, Type type):base(Message)
    {

    }
}