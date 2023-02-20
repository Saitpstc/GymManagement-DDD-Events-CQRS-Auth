namespace GymManagement.API.Models;

public class ApiResponseFactory
{


    public static ApiResponse<T> Success<T>(T data)
    {
        return new ApiResponse<T>
        {
            IsSuccessfull = true,
            Data = data
        };
    }
    public static ApiResponse<T> Fail<T>(List<string> errorMessages)
    {
        return new ApiResponse<T>()
        {
            ErrorMessages = errorMessages,
            IsSuccessfull = false
        };
    }
}