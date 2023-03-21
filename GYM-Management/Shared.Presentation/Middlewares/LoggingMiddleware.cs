namespace Shared.Presentation.Middlewares;

using System.Diagnostics;
using System.Net;
using System.Text;
using Infrastructure;
using Infrastructure.Logger;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Serilog.Core;
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

        LogContext.PushProperty(LogColumns.Endpoint.ToString(), endpoint);
        LogContext.PushProperty(LogColumns.Username.ToString(), userName);
        LogContext.PushProperty(LogColumns.RequestHeaders.ToString(), requestDetailsJson);


        if (string.IsNullOrWhiteSpace(requestBody))
        {
            requestBody = JsonSerializer.Serialize(context.Request.Query);
        }
        LogContext.PushProperty(LogColumns.RequestBody.ToString(), requestBody);

        Stopwatch stopwatch = Stopwatch.StartNew();
        Exception logException = null;


        var responseBodyStream = context.Response.Body;

        string responseBody = "";

        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            // Call the next middleware in the pipeline
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                logException = e;
            }

            // Reset the position of the memory stream to read from the beginning
            memoryStream.Position = 0;

            // Read the response body from the memory stream
            responseBody = new StreamReader(memoryStream).ReadToEndAsync().GetAwaiter().GetResult();

            // Log the response body

            // Copy the contents of the memory stream to the original response body stream
            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(responseBodyStream);
        }



        var statusCode = context.Response.StatusCode;
        var executionTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 4).ToString("0.0000");
        LogContext.PushProperty(LogColumns.ExecutionTime.ToString(), executionTime);
        LogContext.PushProperty(LogColumns.StatusCode.ToString(), statusCode);




        LogContext.PushProperty("Response", responseBody);

        if ((logException != null))
        {
            Log.Error(logException, $"{logException.Message}");
        }

        else
        {
            Log.Information("Request Processed");
        }

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

        StreamReader reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);

        var body = await reader.ReadToEndAsync();

        request.Body.Position = 0;

        return body;
    }
}