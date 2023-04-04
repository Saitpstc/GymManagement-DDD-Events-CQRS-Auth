namespace Authorization_Authentication;

using System.Security.Claims;
using Auth.Entry;
using Infrastructure.Database;
using Microsoft.AspNetCore.Http;

public class AuthService:IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthDbContext _dbContext;

    public AuthService(IHttpContextAccessor httpContextAccessor, AuthDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public SharedUserModel GetCurrentUser()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue("Id");

        var user = _dbContext.Users.Find(userId);
        if (user is null) throw new ArgumentException("User is not found");

        return new SharedUserModel()
        {
            Email = user.Email,
            UserId = userId,
            UserName = user.UserName
        };

    }
}