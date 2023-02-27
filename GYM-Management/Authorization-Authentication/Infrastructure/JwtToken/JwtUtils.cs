namespace Authorization_Authentication.Infrastructure.JwtToken;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class JwtUtils
{
    /// <param name="user">Application's user object</param>
    /// <param name="lifeTimeInMinute"> How many minutes will this token will be valid  </param>
    public static JwtToken CreateToken(JwtUserDto user, int lifeTimeInMinute)
    {
        var claims = SetIdentityClaims(user);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = CreateTokenDescriptor(lifeTimeInMinute, claims);

        var aToken = tokenHandler.CreateToken(tokenDescriptor);

        var aJwtToken = tokenHandler.WriteToken(aToken);

        return new JwtToken(aJwtToken, tokenDescriptor.Expires);
    }

    public static List<Claim> GetUserClaims(string tokenToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var deserializedToken = tokenHandler.ReadJwtToken(tokenToken);
        var claims = deserializedToken.Claims.ToList();

        return claims;
    }

    public static bool IsTokenExpired(string jwtToken)
    {
        jwtToken = jwtToken["Bearer ".Length..];
        var token = new JwtSecurityToken(jwtEncodedString: jwtToken);

        if (DateTime.Compare(DateTime.UtcNow, token.ValidTo) > 0) return true;

        return false;
    }

    private static List<Claim> SetIdentityClaims(JwtUserDto user)
    {
        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("UserName", user.UserName),
            new Claim("Email", user.Email)
        };

        if (user.Roles != null && user.Roles.Any())
        {
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("Role", role));
            }
        }

        if (user.Permissions != null && user.Permissions.Any())
        {
            foreach (var permission in user.Permissions)
            {
                claims.Add(new Claim("Permission", permission));
            }
        }


        return claims;
    }

    private static SecurityTokenDescriptor CreateTokenDescriptor(int lifeTimeInMinute, List<Claim> claims = null)
    {
#pragma warning disable CS1030
#warning when you use this please remove  key from here
#pragma warning restore CS1030
        var key = Encoding.ASCII.GetBytes("Super Secret Token");
        var accessTokenDescriptor = new SecurityTokenDescriptor
        {
            // Set the expiration date for token here
            Expires = DateTime.UtcNow.AddMinutes(lifeTimeInMinute),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        if (claims.Any())
        {
            accessTokenDescriptor.Subject = new ClaimsIdentity(claims);
        }

        return accessTokenDescriptor;
    }
}