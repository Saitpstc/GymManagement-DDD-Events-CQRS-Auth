namespace Shared.Core.Domain;

using System.Net;
using Exceptions;

public class DomainValidationException:BaseException
{
    public DomainValidationException(string message):base(message)
    {
        ErrorMessages.Add(message);
        StatusCode = (int) HttpStatusCode.BadRequest;
    }
}