namespace Auth.Test;

using Authorization_Authentication.Infrastructure.JwtToken;
using Shared.Infrastructure;

public class JwtHelperTest
{




    [Fact]
    public void Create_Token()
    {
        //Arrange
        var role = new List<string>
            { "Admin", "User" };
        var permission = new List<string>
            { "CreateUser", "DeleteUser" };
        JwtUserDto JwtUserDto = new JwtUserDto(Guid.NewGuid(), "test@gmail.com", "UserName")
        {
            Roles = role,
            Permissions = permission
        };

        //act
        JwtToken? token = JwtUtils.CreateToken(JwtUserDto, 60);
        //Assert

        Assert.True(token is not null && token.Token is not null && token.TokenExpireDate is not null);
    }

    [Fact]
    public void GetClaims()
    {
        //Arrange
        //   var helper = new JwtHelper("super secret key");
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

        //Act
        var result = JwtUtils.GetUserClaims(token.Token);

        //Assert
        Assert.True(result.Count(x => x.Type == "Role") == 2 && result.Count(x => x.Type == "Permission") == 2);
    }
}