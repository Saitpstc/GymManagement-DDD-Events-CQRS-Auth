namespace Shared.Presentation.Middlewares;

using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text;
using Core;
using Exceptions;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Serilog.Core.Enrichers;
using Serilog.Events;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;


    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        await PopulateLogContextForRequest(context);


        var stopwatch = Stopwatch.StartNew();
        Exception logException = null;

        try
        {
            await _next(context);

        }
        catch (Exception e)
        {

            var exceptionHandler = new ExceptionHandler(e);

            exceptionHandler.CreateApiResponse().And().ModifyContext().Finally().PopulateLogContext();
            context.Response.Clear();



            await context.Response.WriteAsJsonAsync(apiResponse);

            var response = JsonSerializer.Serialize(apiResponse);
            LogContext.PushProperty("Response", response);

        }

        var statusCode = context.Response.StatusCode;
        LogContext.PushProperty("ExecutionTime", stopwatch.Elapsed.TotalSeconds);
        LogContext.PushProperty("StatusCode", statusCode);

        if (statusCode == (int) HttpStatusCode.Unauthorized)
        {
            Log.Warning("Unauthorized Request Has Been Made");
        }

        else if (statusCode == (int) HttpStatusCode.InternalServerError)
        {
            Log.Error(logException, "An Exception is thrown");

        }
        else
        {
            Log.Information("Request is processed");
        }


        var s = new test();

    }

    static async private Task PopulateLogContextForRequest(HttpContext context)
    {

        var requestDetailsJson = JsonConvert.SerializeObject(context.Request.Headers);

        var requestBody = await GetRawBodyAsync(context.Request);

        var endpoint = context.GetEndpoint()?.DisplayName;

        var userName = context.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;

        LogContext.PushProperty("Endpoint", endpoint);
        LogContext.PushProperty("Username", userName);
        LogContext.PushProperty("RequestHeaders", requestDetailsJson);

        if (string.IsNullOrWhiteSpace(requestBody))
        {
            requestBody = JsonSerializer.Serialize(context.Request.Query);
        }
        LogContext.PushProperty("RequestBody", requestBody);
    }

    public static async Task<string> GetRawBodyAsync(
        HttpRequest request,
        Encoding encoding = null)
    {
        if (!request.Body.CanSeek)
        {
            request.EnableBuffering();
        }

        request.Body.Position = 0;

        var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);

        var body = await reader.ReadToEndAsync();

        request.Body.Position = 0;

        return body;
    }
    public IExceptionHandler CreateApiResponse()
    {
        apiResponse = new ApiResponse()
        {
            IsSuccessfull = false,
            ErrorMessages = Exception.ErrorMessages
        };

        return this;
    }
}

public interface IExceptionHandler
{

    IExceptionHandler ProcessException();

    IExceptionHandler And();

    IExceptionHandler ModifyContext(HttpContext context);

    IExceptionHandler Finally();

    IExceptionHandler PopulateLogContext();

    IExceptionHandler CreateApiResponse();
}

public class ExceptionHandler:IExceptionHandler
{

    private ApiResponse apiResponse { get; set; }
    private BaseException Exception { get; set; }

    public ExceptionHandler(Exception exception)
    {
        Exception = (BaseException) exception;

    }

    public IExceptionHandler ProcessException()
    {


    }

    public IExceptionHandler And()
    {
        throw new NotImplementedException();
    }

    public IExceptionHandler ModifyContext(HttpContext context)
    {
        if (Exception is UnauthorizedRequestException)
        {
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }
        else
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }
    }

    public IExceptionHandler Finally()
    {
        throw new NotImplementedException();
    }

    public IExceptionHandler PopulateLogContext()
    {
        throw new NotImplementedException();
    }

    public IExceptionHandler CreateApiResponse()
    {
        apiResponse = new ApiResponse()
        {
            IsSuccessfull = false,
            ErrorMessages = Exception.ErrorMessages
        };

        return this;
    }
}