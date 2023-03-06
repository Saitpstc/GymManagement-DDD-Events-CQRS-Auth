namespace Shared.Presentation.Middlewares;

using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text;
using Infrastructure;
using Microsoft.AspNetCore.Http;
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


        var stopwatch = Stopwatch.StartNew();
        await _next(context);

        var statusCode = context.Response.StatusCode;
        LogContext.PushProperty("ExecutionTime", stopwatch.Elapsed.TotalSeconds);
        LogContext.PushProperty("StatusCode", statusCode);

        if (statusCode == (int) HttpStatusCode.Unauthorized)
        {
            Log.Warning("Unauthorized Request Has Been Made");
        }

        Log.Information("Request is processed");
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

}