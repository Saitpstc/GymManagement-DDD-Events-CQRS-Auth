namespace Shared.Core.Exceptions;

public class BusinessLogicException:BaseException
{


    public BusinessLogicException(string message):base(message)
    {
        
    }

    public BusinessLogicException(List<string> errorMessages):base(errorMessages)
    {
        
    }
}