﻿namespace GymManagement.API.Controllers.Auth.v1;


using Authorization_Authentication.Application.Contracts;
using Authorization_Authentication.Application.User.Command;
using Authorization_Authentication.Application.User.Query;
using Authorization_Authentication.Dto.User;
using Authorization_Authentication.Infrastructure.JwtToken;
using Authorization_Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Contracts;
using Shared.Infrastructure.Email;
using Shared.Infrastructure.Email.EmailConfirmation;
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
    public async Task<ApiResponse<UserCreatedResponse>> CreateUser(CreateUserCommand createUserCommand)
    {
        var result = await _module.ExecuteCommandAsync(createUserCommand);
        return CreateResponse(result);

    }


    [HttpPost("Login")]
    public async Task<ApiResponse<JwtUserDto>> Login(LoginQuery loginQuery)
    {
        var result = await _module.ExecuteQueryAsync(loginQuery);
        return CreateResponse(result);
    }

    [HttpPost("/ConfirmUser/Code")]
    public async Task<ApiResponse<string>> ConfirmEmailWithCode()
    {
        var result = await _module.ExecuteCommandAsync(new ConfirmEmailCommand(EmailConfirmationTypes.Code));

        return CreateResponse(result);
    }

}