namespace Authorization_Authentication.Infrastructure;

using System.Net;
using JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Shared.Presentation.Exceptions;

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

        var userIsSuperAdmin = context.HttpContext.User.HasClaim(x => x is { Type: "Role", Value: "SuperAdmin" });

         if (userIsSuperAdmin) return;


        StringValues authHeader = context.HttpContext.Request.Headers.Authorization;
        var hasClaim = context.HttpContext.User.HasClaim(x => x.Type == "Permission" && _claimvalue.Contains(x.Value));


        var tokenIsExpired = JwtUtils.IsTokenExpired(authHeader);

        if (string.IsNullOrEmpty(authHeader) || !hasClaim)
        {
            throw new UnauthorizedRequestException("Unauthorized Request Has Been Made ");
        }

        if (tokenIsExpired)
        {

            throw new UnauthorizedRequestException("Token is expired");
        }


    }

    private bool IsLocalRequest(HttpContext context)
    {
        if (context.Request.Host.Value.StartsWith("localhost:")) return true;


        if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null) return true;

        if (context.Connection.RemoteIpAddress != null &&
            context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
        {
            return true;
        }

        return context.Connection.RemoteIpAddress != null && IPAddress.IsLoopback(context.Connection.RemoteIpAddress);
    }
}