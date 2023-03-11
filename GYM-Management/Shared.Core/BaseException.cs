namespace Shared.Core;

public class BaseException:Exception
{
    public List<string> ErrorMessages { get; set; } = new List<string>();

    protected BaseException(string message)
    {

        if (!string.IsNullOrWhiteSpace(message))
        {
            ErrorMessages.Add(message);

        }
        
    }
}