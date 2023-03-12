namespace Shared.Presentation.Models;

public class ApiResponse
{
    public bool IsSuccessfull { get; set; }
    public List<string?>? ErrorMessages { get; set; }
}

public class ApiResponse<T>:ApiResponse
{

    public T? Data { get; set; }
}