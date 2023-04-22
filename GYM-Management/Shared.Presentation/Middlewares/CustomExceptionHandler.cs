namespace Shared.Presentation.Middlewares;

using Microsoft.AspNetCore.Http;
using Models;

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

            ApiResponse apiResponse = ApiResponseFactory.CreateExceptionResponse(e);

            await context.Response.WriteAsJsonAsync(apiResponse);

            throw;
        }

    }
}