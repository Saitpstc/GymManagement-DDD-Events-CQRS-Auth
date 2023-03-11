namespace Shared.Presentation.Exceptions;

using Core;

public class UnauthorizedRequestException:BaseException
{
    public UnauthorizedRequestException(string message):base(message)
    {

    }
}