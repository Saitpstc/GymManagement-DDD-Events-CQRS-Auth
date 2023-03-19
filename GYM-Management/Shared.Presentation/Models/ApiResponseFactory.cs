namespace Shared.Presentation.Models;

using Core;
using Core.Exceptions;

public static class ApiResponseFactory
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
        return new ApiResponse<T>
        {
            ErrorMessages = errorMessages,
            IsSuccessfull = false

        };
    }

    public static ApiResponse CreateExceptionResponse(Exception exception)
    {


        ApiResponse apiResponse = new ApiResponse
        {
            IsSuccessfull = false,
            ErrorMessages = new List<string>()

        };

        if (exception is BaseException)
        {
            var ex = exception as BaseException;
            apiResponse.ErrorMessages = ex.ErrorMessages;
        }
        else
        {
            apiResponse.ErrorMessages.Add(exception.Message);
        }



        return apiResponse;
    }
}