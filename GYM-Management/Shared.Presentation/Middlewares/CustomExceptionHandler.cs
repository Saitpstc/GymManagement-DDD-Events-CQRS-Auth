namespace Shared.Presentation.Middlewares;

using System.Net;
using System.Text.Json;
using Exceptions;
using FluentValidation;
using GymManagement.API.Models;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Models;
using Serilog;
using Serilog.Context;
using Serilog.Events;

public class CustomExceptionHandler
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
           

            /*if (e is UnauthorizedRequestException)
            {
                apiResponse.ErrorMessages = new List<string> { "Unauthorized request" };

                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            }
            else if (e is ValidationException)
            {
                var exception = e as ValidationException;
                apiResponse.ErrorMessages = exception.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                apiResponse.ErrorMessages = new List<string>() { "Internal Server Error Occurred" };

            }
            await context.Response.WriteAsJsonAsync(apiResponse);

            var exSeriazlied = JsonSerializer.Serialize(GetExceptionViewModel(e));
            var response = JsonSerializer.Serialize(apiResponse);
            LogContext.PushProperty("Response", response);
            LogContext.PushProperty("Eception", "sdfasdfad");
            Log.Warning("ExceptionHasBeenThrown");*/
        }
        
    }

    private ExceptionViewModel GetExceptionViewModel(Exception ex)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isDevelopment = environment == Microsoft.AspNetCore.Hosting.EnvironmentName.Development;

        return new ExceptionViewModel()
        {
            ClassName = !isDevelopment ? "Exception" : ex.GetType().Name.Split('.').Reverse().First(),
            InnerException = ex.InnerException != null ? GetExceptionViewModel(ex.InnerException) : null,
            Message = !isDevelopment ? "Internal Server Error" : ex.Message,
            StackTrace = !isDevelopment ? new List<string>() : ex.StackTrace.Split(Environment.NewLine).ToList()
        };
    }
}