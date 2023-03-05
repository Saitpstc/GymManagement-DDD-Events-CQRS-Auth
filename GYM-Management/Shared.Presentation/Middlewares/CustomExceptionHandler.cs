namespace Shared.Presentation.Middlewares;

using FluentValidation;
using GymManagement.API.Models;
using Microsoft.AspNetCore.Http;

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
            if (e is ValidationException)
            {
                var exception = e as ValidationException;
                context.Response.Clear();
                var apiResponse = new ApiResponse()
                {
                    ErrorMessages = exception.Errors.Select(x => x.ErrorMessage).ToList(),
                    IsSuccessfull = false
                };
                await context.Response.WriteAsJsonAsync(apiResponse);

            }
        }
    }
}