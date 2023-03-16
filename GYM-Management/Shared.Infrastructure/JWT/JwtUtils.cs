namespace Shared.Infrastructure.JWT;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtUtils
{

    /// <param name="user">Application's user object</param>
    /// <param name="lifeTimeInMinute"> How many minutes will this token will be valid  </param>
    public static JwtToken CreateToken(JwtUserDto user, int lifeTimeInMinute)
    {
        var claims = SetIdentityClaims(user);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();


        SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(lifeTimeInMinute, claims);


        SecurityToken? aToken = tokenHandler.CreateToken(tokenDescriptor);

        var aJwtToken = tokenHandler.WriteToken(aToken);

        return new JwtToken(aJwtToken, tokenDescriptor.Expires);
    }

    public static List<Claim> GetUserClaims(string tokenToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken? deserializedToken = tokenHandler.ReadJwtToken(tokenToken);
        var claims = deserializedToken.Claims.ToList();

        return claims;
    }

    public static bool IsTokenExpired(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken)) return false;
        jwtToken = jwtToken["Bearer ".Length..];
        JwtSecurityToken token = new JwtSecurityToken(jwtEncodedString: jwtToken);

        if (DateTime.Compare(DateTime.UtcNow, token.ValidTo) > 0) return true;

        return false;
    }

    static private Dictionary<string, object> SetIdentityClaims(JwtUserDto user)
    {
        var claims = new Dictionary<string, object>
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
        SecurityTokenDescriptor accessTokenDescriptor = new SecurityTokenDescriptor
        {
            // Set the expiration date for token here
            Expires = DateTime.UtcNow.AddMinutes(lifeTimeInMinute),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };

        if (claims.Any())
        {
            accessTokenDescriptor.Claims = claims;
        }

        return accessTokenDescriptor;
    }
}