namespace Authorization_Authentication.Middlewares;

using System.Net;
using System.Text;
using Infrastructure.JwtToken;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Shared.Presentation.Attributes;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var authorizeAttribute = context.GetEndpoint()?.Metadata?.GetMetadata<AuthorizeFilter>();

        if (authorizeAttribute is null)
        {
            await _next.Invoke(context);
            return;
        }

        var authHeader = context.Request.Headers.Authorization;

        if (!string.IsNullOrEmpty(authHeader))
        {
            var tokenIsExpired = JwtUtils.IsTokenExpired(authHeader);

            if (!tokenIsExpired)
            {
                LogContext.PushProperty("StatusCode", (int) HttpStatusCode.OK);

                Log.Warning($"Token Expired");
                context.Response.Clear();
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                await context.Response.WriteAsync("Token is expired");
                return;
            }
        }

        await _next.Invoke(context);
    }



}