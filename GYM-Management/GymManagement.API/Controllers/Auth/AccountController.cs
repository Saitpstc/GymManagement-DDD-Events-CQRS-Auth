namespace GymManagement.API.Controllers.Auth;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Authorization_Authentication.Attributes;
using Authorization_Authentication.Infrastructure.JwtToken;
using Authorization_Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Shared.Application.Contracts;

[Route("Auth")]
public class AccountController:BaseController
{
    private readonly UserManager<User> _manager;

    public AccountController(IErrorMessageCollector collector, UserManager<User> manager):base(collector)
    {
        _manager = manager;
    }

    [HttpPost]
    public async Task<ApiResponse<User>> CreateAccount(UserReqDto dto)
    {

        var user = new User()
        {
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _manager.CreateAsync(user);

        return CreateResponse(user);
    }

    [HttpPost("login")]
    public async Task<ApiResponse<JwtUserDto>> login(UserReqDto dto)
    {

        var dt = new JwtUserDto(new Guid("89B4B112-3E93-4A05-EAB2-08DB18C46A04"), "user@example.com", "user@example.com");
        var result = JwtUtils.CreateToken(dt, 60);

        dt.Token = result;

        return CreateResponse(dt);
    }

    [HttpPost("test")]
    [AuthorizeFilter("Account")]
    public async Task<ApiResponse<string>> authorizeTest(string a = "sait")
    {
        var test = new string("asdfasdf");

        return CreateResponse("asdfasdf");
    }
}

public class UserReqDto
{
    [EmailAddress] [Required] public string Email { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DefaultValue("P@ssw0rd1")]
    public string Password { get; set; }


}