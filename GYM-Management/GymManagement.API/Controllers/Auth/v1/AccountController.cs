namespace GymManagement.API.Controllers.Auth.v1;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Authorization_Authentication.Application.Contracts;
using Authorization_Authentication.Application.Superadmin.Commands;
using Authorization_Authentication.Application.User;
using Authorization_Authentication.Dto;
using Authorization_Authentication.Dto.User;
using Authorization_Authentication.Infrastructure.JwtToken;
using Authorization_Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Contracts;
using Shared.Infrastructure;
using Shared.Presentation.Attributes;
using Shared.Presentation.Models;

[Route("Auth")]
public class AccountController:BaseController
{
    private readonly IAuthModule _module;

    public AccountController(IErrorMessageCollector collector, UserManager<User> manager, IAuthModule module):base(collector)
    {
        _module = module;
    }

    [HttpPost("CreateUser")]
    public async Task<ApiResponse<UserCreatedResponse>> CreateRole(CreateUserCommand dto)
    {
        var result = await _module.ExecuteCommandAsync(dto);
        return CreateResponse(result);

    }

    [AuthorizeFilter("test")]
    [HttpGet("test")]
    public async Task<ApiResponse<JwtUserDto>> login()
    {

        throw new NotImplementedException();
        JwtUserDto dt = new JwtUserDto(new Guid("89B4B112-3E93-4A05-EAB2-08DB18C46A04"), "user@example.com", "user@example.com");
        JwtToken result = JwtUtils.CreateToken(dt, 60);

        dt.Token = result;

        return CreateResponse(dt);
    }
}
/*
    [HttpPost("test")]
    [AuthorizeFilter("Account")]
    public async Task<ApiResponse<string>> authorizeTest(string b = "sait2", string a = "sait")
    {

        throw new NotImplementedException();
    }
}

public class UserReqDto
{
    [EmailAddress] [Required] public string Email { get; set; }

    [Required]
    [StringLength(16, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DefaultValue("P@ssw0rd1")]
    public string Password { get; set; }
}*/