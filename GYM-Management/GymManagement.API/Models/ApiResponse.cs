namespace GymManagement.API.Models;

public class ApiResponse<T>
{
    public bool IsSuccessfull { get; set; }
    public T Data { get; set; }
    public List<string> ErrorMessages { get; set; }

    public static ApiResponse<T> Fail(List<string> errorMessages)
    {
        return new ApiResponse<T>()
        {
            ErrorMessages = errorMessages,
            IsSuccessfull = false
        };
    }

    public static ApiResponse<T> Success(T T)
    {
        return new ApiResponse<T>()
        {
            Data = T,
            IsSuccessfull = true
        };
    }
}