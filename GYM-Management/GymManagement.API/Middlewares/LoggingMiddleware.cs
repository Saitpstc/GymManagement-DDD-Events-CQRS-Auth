namespace GymManagement.API.Middlewares;

using System.Diagnostics;
using System.Text;
using Azure.Core;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        // Log request details
        var requestDetails = new
        {
            context.Request.Method,
            context.Request.Path,
            context.Request.QueryString,
            context.Request.Headers,
            Request = await GetRequestBodyAsync(context.Request)
        };

        var requestDetailsJson = JsonConvert.SerializeObject(requestDetails);


        var stopwatch = Stopwatch.StartNew();
        // Call the next middleware
        await _next(context);


        stopwatch.Stop();
        // Log response details
        var responseDetails = new
        {
            context.Response.StatusCode,
            context.Response.Headers,
            ExecutionTime = stopwatch.Elapsed
        };

        
        var responseDetailsJson = JsonConvert.SerializeObject(responseDetails);

        Log.ForContext("Request",requestDetailsJson).ForContext("Response",responseDetailsJson).Information("a request has been started");
    }

    private async Task<string> GetRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var requestBody = await reader.ReadToEndAsync();

        request.Body.Position = 0;

        return requestBody;
    }


}