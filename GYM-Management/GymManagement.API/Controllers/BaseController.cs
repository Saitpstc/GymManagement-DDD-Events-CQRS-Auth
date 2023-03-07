﻿namespace GymManagement.API.Controllers;

using global::Customer.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Models;
using Shared.Application.Contracts;
using Shared.Presentation.Models;

[ApiController]

public class BaseController
{
    private readonly ErrorMessageCollector _collector;

    public BaseController(IErrorMessageCollector collector)
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