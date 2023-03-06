namespace Shared.Presentation.Attributes;

using System.Net;
using System.Text.Json;
using GymManagement.API.Models;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;

public class AuthorizeFilter:Attribute, IAuthorizationFilter
{

    private readonly string[] _claimvalue;

    public AuthorizeFilter(params string[] claimvalue)
    {

        _claimvalue = claimvalue;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {

        //Uncomment this code if you dont want to require authorization for local requests
        //if (IsLocalRequest(context.HttpContext)) return;


        var authHeader = context.HttpContext.Request.Headers.Authorization;
        var hasClaim = context.HttpContext.User.HasClaim(x => x.Type == "Permission" && _claimvalue.Contains(x.Value));

        if (string.IsNullOrEmpty(authHeader) || !hasClaim)
        {
            var apiResponse = new ApiResponse()
            {
                IsSuccessfull = false,
                ErrorMessages = new List<string>() { "Unauthorized request" }
            };
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            context.HttpContext.Response.WriteAsJsonAsync(apiResponse);

            LogContext.PushProperty("Response", JsonSerializer.Serialize(apiResponse));

        }


    }
    
    private bool IsLocalRequest(HttpContext context)
    {
        if (context.Request.Host.Value.StartsWith("localhost:")) return true;


        if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null) return true;

        if (context.Connection.RemoteIpAddress != null &&
            context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress)) return true;

        return context.Connection.RemoteIpAddress != null && IPAddress.IsLoopback(context.Connection.RemoteIpAddress);
    }
}