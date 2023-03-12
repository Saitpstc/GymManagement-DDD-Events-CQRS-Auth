namespace Shared.Core.Exceptions;

public class RequestValidationException:BaseException
{

    public RequestValidationException(string message):base(message)
    {
    }

    public RequestValidationException(List<string> errorMessages):base(errorMessages)
    {
    }
}