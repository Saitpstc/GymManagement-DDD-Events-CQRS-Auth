namespace Shared.Presentation.Exceptions;

using Core.Exceptions;

public class UnauthorizedRequestException:BaseException
{
    public UnauthorizedRequestException(string? message):base(message)
    {

    }
}