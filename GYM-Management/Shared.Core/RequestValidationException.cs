namespace Shared.Core;

public class RequestValidationException:BaseException
{

    public RequestValidationException(string? message=null, List<string>? errorMessages = null):base(message)
    {
        if (errorMessages != null)
        { 
            ErrorMessages = errorMessages;
        }


    }

 
}