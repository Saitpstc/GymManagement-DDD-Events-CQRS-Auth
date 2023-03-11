namespace Shared.Presentation.Middlewares;

using System.Net;
using System.Text.Json;
using Core;
using Exceptions;
using FluentValidation;
using GymManagement.API.Models;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Models;
using Serilog;
using Serilog.Context;
using Serilog.Events;

public class CustomExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ISerilogContext _serilogContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CustomExceptionHandler(RequestDelegate next, ISerilogContext serilogContext, IWebHostEnvironment webHostEnvironment)
    {
        _next = next;
        _serilogContext = serilogContext;
        _webHostEnvironment = webHostEnvironment;
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

            var apiResponse = ApiResponseFactory.CreateExceptionResponse(e);

            if (e is UnauthorizedRequestException)
            {

                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            }
            else if(e is RequestValidationException)
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