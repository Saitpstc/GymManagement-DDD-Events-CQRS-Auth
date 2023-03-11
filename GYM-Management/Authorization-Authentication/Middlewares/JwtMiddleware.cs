namespace Authorization_Authentication.Middlewares;

using System.Net;
using System.Text;
using GymManagement.API.Models;
using Infrastructure.JwtToken;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Shared.Infrastructure;
using Shared.Presentation.Attributes;
using Shared.Presentation.Exceptions;
using Shared.Presentation.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISerilogContext _serilogContext;


    public JwtMiddleware(RequestDelegate next, ISerilogContext serilogContext)
    {
        _next = next;
        _serilogContext = serilogContext;

    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authorizeAttribute = context.GetEndpoint()?.Metadata?.GetMetadata<AuthorizeFilter>();


        if (authorizeAttribute is not null)
        {
            var authHeader = context.Request.Headers.Authorization;

            if (!string.IsNullOrEmpty(authHeader))
            {
                var tokenIsExpired = JwtUtils.IsTokenExpired(authHeader);

                if (!tokenIsExpired)
                {
                    var response = new ApiResponse()
                    {
                        ErrorMessages = new List<string>() { "Token is expired" },
                        IsSuccessfull = false
                    };
                    await CreateUnauthorizedUserResponse(context, response);
                    return;
                }
            }
        }
        await _next.Invoke(context);
    }



    static async private Task CreateUnauthorizedUserResponse(HttpContext context, ApiResponse response)
    {

        context.Response.Clear();
        context.Response.StatusCode = (int) HttpStatusCode.OK;
        await context.Response.WriteAsJsonAsync(response);
    }

}