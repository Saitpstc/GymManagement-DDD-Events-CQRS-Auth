﻿namespace Shared.Presentation.Middlewares;

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
    private readonly ISerilogContext _serilogContext;


    public LoggingMiddleware(RequestDelegate next, ISerilogContext serilogContext)
    {
        _next = next;
        _serilogContext = serilogContext;
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
            logException = e;

        }

        var statusCode = context.Response.StatusCode;
        _serilogContext.PushToLogContext(LogColumns.ExecutionTime, Math.Round(stopwatch.Elapsed.TotalSeconds, 4).ToString("0.0000"));
        _serilogContext.PushToLogContext(LogColumns.StatusCode, statusCode);

        
        LogContext.Push(_serilogContext.GetEnricher());

        if (logException is not null)
        {
            Log.Error(logException, "An Exception Has Been Thrown");
        }
        else
        {
            Log.Information("Request has been processed");
        }
        _serilogContext.DisposeContext();


    }


    async private Task PopulateLogContextForRequest(HttpContext context)
    {
        var requestDetailsJson = JsonConvert.SerializeObject(context.Request.Headers);

        var requestBody = await GetRawBodyAsync(context.Request);

        var endpoint = context.GetEndpoint()?.DisplayName;

        var userName = context.User.Claims.FirstOrDefault(x => x.Type == "UserName")?.Value;

        _serilogContext.PushToLogContext(LogColumns.Endpoint, endpoint);
        _serilogContext.PushToLogContext(LogColumns.Username, userName);
        _serilogContext.PushToLogContext(LogColumns.RequestHeaders, requestDetailsJson);


        if (string.IsNullOrWhiteSpace(requestBody))
        {
            requestBody = JsonSerializer.Serialize(context.Request.Query);
        }
        _serilogContext.PushToLogContext(LogColumns.RequestBody, requestBody);

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