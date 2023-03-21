namespace Shared.Core.Exceptions;

using System.Net;

public class BusinessLogicException:BaseException
{


    public BusinessLogicException(string message):base(message)
    {

    }

    public BusinessLogicException(List<string> errorMessages):base(errorMessages)
    {
     
    }
}