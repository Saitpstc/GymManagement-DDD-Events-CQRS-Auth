namespace Shared.Core.Exceptions;

public class BaseException:Exception
{

    protected BaseException(string message)
    {

        if (!string.IsNullOrWhiteSpace(message))
        {
            ErrorMessages.Add(message);

        }

    }

    protected BaseException(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public List<string> ErrorMessages { get; set; } = new List<string>();
    public int StatusCode { get; protected set; }
}