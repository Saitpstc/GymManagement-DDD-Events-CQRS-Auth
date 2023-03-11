namespace Shared.Presentation.Middlewares;

using System.Net;
using Core;
using Exceptions;
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

            context.Response.Clear();

            ApiResponse apiResponse = ApiResponseFactory.CreateExceptionResponse(e);

            if (e is UnauthorizedRequestException)
            {

                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            }
            else if (e is RequestValidationException)
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            }
            await context.Response.WriteAsJsonAsync(apiResponse);

            throw;
        }

    }
}