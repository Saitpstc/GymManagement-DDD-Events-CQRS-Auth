namespace Authorization_Authentication.Application.User.Query;

using FluentValidation;
using Infrastructure.JwtToken;
using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Application.Contracts;
using Shared.Core.Exceptions;
using Shared.Infrastructure;

public class LoginQuery:IQuery<JwtUserDto>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginQueryValidator:AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty").MinimumLength(6)
                                .WithMessage("Password should be at least 6 character");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty");
    }
}

public class LoginQueryCommandHandler:QueryHandlerBase<LoginQuery, JwtUserDto>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public LoginQueryCommandHandler(
        IErrorMessageCollector errorMessageCollector,
        UserManager<User> userManager,
        SignInManager<User> signInManager):base(errorMessageCollector)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public override async Task<JwtUserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
        var user = _userManager.Users.FirstOrDefault(x => x.UserName == request.UserName);
        

        if (!result.Succeeded)
        {
            if (!user.EmailConfirmed)
            {
                throw new BusinessLogicException("Email is not confirmed");
            }
            throw new BusinessLogicException("Login attempt failed");
            
        }


        var jwtuserDto = new JwtUserDto(user.Id, user.UserName, user.Email);
        var token = JwtUtils.CreateToken(jwtuserDto, 60);

        jwtuserDto.Token = token;
        return jwtuserDto;
    }
}