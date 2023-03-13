namespace Authorization_Authentication.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Core.Contracts;

public class UserService:IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;

    }
}