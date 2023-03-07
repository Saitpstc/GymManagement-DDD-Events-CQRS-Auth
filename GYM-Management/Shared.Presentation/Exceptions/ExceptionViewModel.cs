namespace Shared.Presentation.Exceptions;

public class ExceptionViewModel
{
    public string ClassName { get; set; }
    public string Message { get; set; }
    public ExceptionViewModel InnerException { get; set; }
    public List<string> StackTrace { get; set; }
}