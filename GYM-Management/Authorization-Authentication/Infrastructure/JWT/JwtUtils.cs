﻿namespace Authorization_Authentication.Infrastructure.JWT;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models;

class JwtUtils
{

    /// <param name="user">Application's user object</param>
    /// <param name="lifeTimeInMinute"> How many minutes will this token will be valid  </param>
    public static JwtUserDto CreateToken(User user, int lifeTimeInMinute)
    {
        JwtUserDto userDto = new JwtUserDto();
        var claims = SetIdentityClaims(user, userDto);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();


        SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(lifeTimeInMinute, claims);


        SecurityToken? aToken = tokenHandler.CreateToken(tokenDescriptor);

        var aJwtToken = tokenHandler.WriteToken(aToken);

        userDto.UserName = user.UserName;
        userDto.Id = user.Id;
        userDto.Email = user.Email;
        userDto.Token = new JwtToken(aJwtToken, tokenDescriptor.Expires);
        return userDto;
    }

    public static List<Claim> GetUserClaims(string tokenToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken? deserializedToken = tokenHandler.ReadJwtToken(tokenToken);
        var claims = deserializedToken.Claims.ToList();

        return claims;
    }

    /// <summary>
    ///     returns false if token
    /// </summary>
    /// <param name="jwtToken"></param>
    /// <returns></returns>
    public static bool IsTokenExpired(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
        {
            return true;
        }

        jwtToken = jwtToken["Bearer ".Length..];
        JwtSecurityToken token = new JwtSecurityToken(jwtEncodedString: jwtToken);

        if (DateTime.Compare(DateTime.UtcNow, token.ValidTo) > 0) return false;

        return true;
    }

    static private Dictionary<string, object> SetIdentityClaims(User user, JwtUserDto jwtUserDto)
    {
        var claims = new Dictionary<string, object>
        {
            { "Id", user.Id.ToString() },
            { "UserName", user.UserName },
            { "Email", user.Email }
        };

        var roles = user.UserRoles.Select(x => x.Role).ToList();

        if (roles.Any())
        {
            foreach (Role role in roles.Where(x => x.IsActive))
            {

                claims.Add("Role", role.Name);
                jwtUserDto.Roles.Add(role.Name);
            }
        }


        var permissions = user.UserRoles.Select(x => x.Role).SelectMany(x => x.RolePermissionMaps).ToList();

        if (permissions.Any())
        {
            foreach (RolePermission permission in permissions)
            {
                var normalizedPermission = $"{permission.PermissionContext.ToString()}.{permission.PermissionType.ToString()}";
                claims.Add("Permission", normalizedPermission);
                jwtUserDto.Permissions.Add(normalizedPermission);

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