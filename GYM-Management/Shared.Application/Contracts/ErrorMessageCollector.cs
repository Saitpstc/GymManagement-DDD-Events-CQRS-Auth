namespace Shared.Application.Contracts;

public class ErrorMessageCollector:IErrorMessageCollector
{
    public List<string> ErrorMessage { get; } = new List<string>();

    public void AddError(string errorMessage)
    {
        ErrorMessage.Add(errorMessage);
    }

    public void RemoveErrors()
    {
        ErrorMessage.Clear();
    }
    
}

public interface IErrorMessageCollector
{
    void AddError(string errorMessage);
}