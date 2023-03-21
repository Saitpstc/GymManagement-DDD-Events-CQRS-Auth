namespace Shared.Presentation.Exceptions;

using System.Net;
using Core;
using Core.Exceptions;

public class UnauthorizedRequestException:BaseException
{
    public UnauthorizedRequestException(string? message):base(message)
    {

    }
}