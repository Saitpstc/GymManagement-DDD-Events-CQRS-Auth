namespace GymManagement.API.Controllers;

using global::Customer.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Models;
using Shared.Application.Contracts;

[ApiController]
public class BaseController
{
    private readonly ErrorMessageCollector _collector;

    protected BaseController(IErrorMessageCollector collector)
    {
        _collector = (ErrorMessageCollector) collector;

    }

    protected ApiResponse<T> CreateResponse<T>(T response)
    {
        if (_collector.ErrorMessage.Any())
        {
            return ApiResponseFactory.Fail<T>(_collector.ErrorMessage);
        }
        
        return ApiResponseFactory.Success(response);
    }

}