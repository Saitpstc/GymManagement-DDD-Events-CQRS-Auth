namespace Shared.Presentation.Middlewares;

using System.Net;
using Core;
using Core.Exceptions;
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

            ApiResponse apiResponse = ApiResponseFactory.CreateExceptionResponse(e);


            if (e is BaseException)
            {

                var ex = (BaseException) e;

                if (ex.StatusCode == 0)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Accepted;
                }
                else
                {
                    context.Response.StatusCode = ex.StatusCode;
                }


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