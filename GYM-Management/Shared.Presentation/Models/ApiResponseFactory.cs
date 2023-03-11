namespace Shared.Presentation.Models;

using Shared.Core;

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
        return new ApiResponse<T>()
        {
            ErrorMessages = errorMessages,
            IsSuccessfull = false,

        };
    }

    public static ApiResponse CreateExceptionResponse(Exception exception)
    {
        var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development",
            StringComparison.InvariantCultureIgnoreCase);

        var apiResponse = new ApiResponse()
        {
            IsSuccessfull = false,
            ErrorMessages = new List<string>()

        };

        if (!isDevelopment)
        {
            apiResponse.ErrorMessages.Add("Internal Server Error");
        }
        else
        {
            if (exception is BaseException)
            {
                var e = (BaseException) exception;
                apiResponse.ErrorMessages = e.ErrorMessages;
            }
            else
            {
                apiResponse.ErrorMessages.Add(exception.Message);
            }

        }


        return apiResponse;
    }
}