namespace Authorization_Authentication.Application.User.Query;

using Events;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    private readonly AuthDbContext _context;

    private readonly IMediator _mediator;


    public LoginQueryCommandHandler(
        IErrorMessageCollector errorMessageCollector,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        AuthDbContext context,
        IMediator mediator
    ):base(errorMessageCollector)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _mediator = mediator;

    }

    public override async Task<JwtUserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
        var user = _userManager.Users
                               .Include(x => x.UserRoles)
                               .ThenInclude(x => x.Role)
                               .ThenInclude(x => x.RolePermissionMaps)
                               .ThenInclude(x => x.Permission)
                               .FirstOrDefault(x => x.UserName == request.UserName);

        if (user is null)
        {
            throw new BusinessLogicException("User is not found");
        }


        if (!result.Succeeded)
        {
            if (!user.EmailConfirmed)
            {
                await _mediator.Publish(new EmailConfirmationEvent()
                {
                    UserName = user.UserName
                });
                throw new BusinessLogicException("Email is not confirmed");
            }
            throw new BusinessLogicException("Login attempt failed");

        }


        var userDto = JwtUtils.CreateToken(user, 60);
       
        return userDto;
    }
}