namespace Shared.Core.Exceptions;

using System.Net;

public class BusinessLogicException:BaseException
{


    public BusinessLogicException(string message):base(message)
    {
        StatusCode = (int) HttpStatusCode.BadRequest;
    }

    public BusinessLogicException(List<string> errorMessages):base(errorMessages)
    {
        StatusCode = (int) HttpStatusCode.BadRequest;
    }
}