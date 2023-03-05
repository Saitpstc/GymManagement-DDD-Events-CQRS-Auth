namespace Auth.Test;

using System.Net;
using Authorization_Authentication.Infrastructure.JwtToken;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Shared.Presentation.Attributes;
using Xunit.Abstractions;
using RouteData = Microsoft.AspNetCore.Routing.RouteData;

public class AuthorizationFilterTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AuthorizationFilterTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Forbid_User_If_User_Does_Not_Have_Specified_Permissions()
    {
        var authorizationFilter = CreateAuthorizationFilter("CreateAccount");
        var context = CreateMockHttpContext(authorizationFilter);
        var token = CreateTokenWithDummyUser(context);
        context.HttpContext.Request.Headers.Authorization = token;
        authorizationFilter.OnAuthorization(context);

        var result = context;


        Assert.True(result.HttpContext.Response.StatusCode == (int) HttpStatusCode.Forbidden && result.Result.GetType() == typeof(ForbidResult));
    }




    private string CreateTokenWithDummyUser(AuthorizationFilterContext context)
    {
        var role = new List<string>() { "Admin", "User" };
        var permission = new List<string>()
            { "CreateUser", "DeleteUser" };
        var JwtUserDto = new JwtUserDto(Guid.NewGuid(), "test@gmail.com", "UserName")
        {
            Roles = role,
            Permissions = permission
        };

        var token = JwtUtils.CreateToken(JwtUserDto, 60);
        return token.Token;
    }

    private AuthorizeFilter CreateAuthorizationFilter(string roleOrPermission)
    {
        return new AuthorizeFilter(roleOrPermission);
    }

    private AuthorizationFilterContext CreateMockHttpContext(AuthorizeFilter authorizationFilter)
    {
        var actionContext = new ActionContext();
        actionContext.HttpContext = new DefaultHttpContext();
        actionContext.RouteData = new RouteData();
        actionContext.ActionDescriptor = new ActionDescriptor();
        var context = new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>() { authorizationFilter });

        return context;
    }
}