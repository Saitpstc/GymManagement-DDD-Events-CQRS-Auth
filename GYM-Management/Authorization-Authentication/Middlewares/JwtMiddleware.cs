namespace Authorization_Authentication.Middlewares;

using System.Net;
using Attributes;
using Authorization_Authentication.Infrastructure.JwtToken;
using Microsoft.AspNetCore.Http;

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
            if (tokenIsExpired)
            {
                //context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync("Token is expired");
                
                return;
            }
            
            
        }
        else
        {
           // context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");

            return;
        }

        await _next.Invoke(context);
    }
}