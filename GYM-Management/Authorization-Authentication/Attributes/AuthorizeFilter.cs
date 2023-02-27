namespace Authorization_Authentication.Attributes;

using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizeFilter : Attribute, IAuthorizationFilter
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
  
        
        var hasClaim = context.HttpContext.User.HasClaim(x => x.Type == "Permission" && _claimvalue.Contains(x.Value));
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
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