namespace Shared.Infrastructure;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization_Authentication.Infrastructure.JwtToken;
using Microsoft.IdentityModel.Tokens;

public  class JwtUtils
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

    private static Dictionary<string, object> SetIdentityClaims(JwtUserDto user)
    {
        var claims = new Dictionary<string, object>()
        {
            { "Id", user.Id.ToString() },
            { "UserName", user.UserName },
            { "Email", user.Email }
        };

        if (user.Roles != null && user.Roles.Any())
        {
            foreach (var role in user.Roles)
            {
                claims.Add("Role", role);
            }
        }

        if (user.Permissions != null && user.Permissions.Any())
        {
            foreach (var permission in user.Permissions)
            {
                claims.Add("Permission", permission);
            }
        }


        return claims;
    }

    static private SecurityTokenDescriptor CreateTokenDescriptor(int lifeTimeInMinute, Dictionary<string, object> claims = null)
    {

        var key = Encoding.ASCII.GetBytes("GymManagementAppTokenKey");
        var accessTokenDescriptor = new SecurityTokenDescriptor
        {
            // Set the expiration date for token here
            Expires = DateTime.UtcNow.AddMinutes(lifeTimeInMinute),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            
        };

        if (claims.Any())
        {
            accessTokenDescriptor.Claims = claims;
        }

        return accessTokenDescriptor;
    }
}