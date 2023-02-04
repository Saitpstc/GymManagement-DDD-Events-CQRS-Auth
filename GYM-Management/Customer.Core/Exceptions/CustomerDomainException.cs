namespace Customer.Core.Exceptions;

internal class CustomerDomainException:Exception
{
    public CustomerDomainException(string Message, Type type):base(Message)
    {

    }
}