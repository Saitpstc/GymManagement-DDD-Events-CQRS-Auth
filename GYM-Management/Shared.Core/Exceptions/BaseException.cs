namespace Shared.Core;

public class BaseException:Exception
{

    protected BaseException(string message)
    {

        if (!string.IsNullOrWhiteSpace(message))
        {
            ErrorMessages.Add(message);

        }

    }

    public List<string> ErrorMessages { get; set; } = new List<string>();
}