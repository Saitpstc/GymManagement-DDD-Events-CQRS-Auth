namespace Auth.Test;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Authorization_Authentication.Infrastructure.JwtToken;
using Authorization_Authentication.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Shared.Infrastructure;

public class MiddlewareTest
{
    //todo: create tests
    [Fact]
    public async Task Unauthorized_Response_Result_If_Token_Not_Exist()
    {
        JwtMiddleware middleware = new JwtMiddleware(null, new SerilogContext());

        DefaultHttpContext context = new DefaultHttpContext();
        JwtUserDto JwtUserDto = new JwtUserDto(Guid.NewGuid(), "test@gmail.com", "UserName");

        await middleware.InvokeAsync(context);



        Assert.True(context.HttpContext.Response.StatusCode == 401);
    }

    [Fact]
    public async Task Token_Expired_Response_Result_If_Token_Expired()
    {
        JwtMiddleware middleware = new JwtMiddleware(null, new SerilogContext());

        DefaultHttpContext context = new DefaultHttpContext();

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("Super Secret Token");
        SecurityTokenDescriptor accessTokenDescriptor = new SecurityTokenDescriptor
        {
            // Set the expiration date for token here
            Expires = DateTime.UtcNow.AddMilliseconds(100),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken? aToken = tokenHandler.CreateToken(accessTokenDescriptor);
        var aJwtToken = tokenHandler.WriteToken(aToken);

        context.Request.Headers.Authorization = aJwtToken;

        await Task.Delay(TimeSpan.FromMilliseconds(100));


        await middleware.InvokeAsync(context);


        Stream responseBodyStream = context.Response.Body;
        responseBodyStream.Seek(0, SeekOrigin.Begin);

        using StreamReader streamReader = new StreamReader(responseBodyStream, Encoding.UTF8);
        var responseBody = await streamReader.ReadToEndAsync();

        HttpClient client = new HttpClient();


        Assert.True(context.HttpContext.Response.StatusCode == 200);
    }
}