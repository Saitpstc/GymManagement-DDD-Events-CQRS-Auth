namespace Authorization_Authentication;

using System.Security.Claims;
using Auth.Entry;
using Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Models;

class AuthService:IAuthService
{
    private readonly AuthDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor, AuthDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public UserModel GetCurrentUser()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue("Id");

        User? user = _dbContext.Users.Find(userId);
        if (user is null) throw new ArgumentException("User is not found");

        return new UserModel
        {
            Email = user.Email,
            UserId = userId,
            UserName = user.UserName
        };

    }
}