namespace Auth.Test;

using System.Net;
using Authorization_Authentication.Infrastructure;
using Authorization_Authentication.Infrastructure.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Shared.Infrastructure;
using Xunit.Abstractions;

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
        AuthorizeFilter authorizationFilter = CreateAuthorizationFilter("CreateAccount");
        AuthorizationFilterContext context = CreateMockHttpContext(authorizationFilter);
        var token = CreateTokenWithDummyUser(context);
        context.HttpContext.Request.Headers.Authorization = token;
        authorizationFilter.OnAuthorization(context);

        AuthorizationFilterContext result = context;


        Assert.True(result.HttpContext.Response.StatusCode == (int) HttpStatusCode.Forbidden && result.Result.GetType() == typeof(ForbidResult));
    }




    private string CreateTokenWithDummyUser(AuthorizationFilterContext context)
    {
        var role = new List<string>
            { "Admin", "User" };
        var permission = new List<string>
            { "CreateUser", "DeleteUser" };
        JwtUserDto JwtUserDto = new JwtUserDto(Guid.NewGuid(), "test@gmail.com", "UserName")
        {
            Roles = role,
            Permissions = permission
        };

        JwtToken token = JwtUtils.CreateToken(JwtUserDto, 60);
        return token.Token;
    }

    private AuthorizeFilter CreateAuthorizationFilter(string roleOrPermission)
    {
        return new AuthorizeFilter(roleOrPermission);
    }

    private AuthorizationFilterContext CreateMockHttpContext(AuthorizeFilter authorizationFilter)
    {
        ActionContext actionContext = new ActionContext();
        actionContext.HttpContext = new DefaultHttpContext();
        actionContext.RouteData = new RouteData();
        actionContext.ActionDescriptor = new ActionDescriptor();
        AuthorizationFilterContext context = new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>
            { authorizationFilter });

        return context;
    }
}