namespace Authorization_Authentication.Application.User.Command;

using Dto.User;
using Events;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Application.Contracts;
using Shared.Core.Exceptions;

public class CreateUserCommand:ICommand<UserCreatedResponse>
{
    public string? UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty").MinimumLength(6)
                                .WithMessage("Password should be at least 6 character");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is not valid");
    }
}

class CreateUserCommandHandler:CommandHandlerBase<CreateUserCommand, UserCreatedResponse>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(IMediator mediator, IErrorMessageCollector errorMessageCollector, UserManager<User> userManager):base(
        errorMessageCollector)
    {
        _mediator = mediator;
        _userManager = userManager;
    }


    public override async Task<UserCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User
        {
            Email = request.Email

        };

        if (!string.IsNullOrEmpty(request.UserName))
        {
            user.UserName = request.UserName;
        }
        else
        {
            user.UserName = request.Email;
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
        IdentityResult? result = await _userManager.CreateAsync(user);


        if (!result.Succeeded)
        {
            throw new BusinessLogicException(result.Errors.Select(x => x.Description).ToList());
        }
        await _mediator.Publish(new EmailConfirmationEvent
        {
            UserName = user.UserName
        }, cancellationToken);


        return new UserCreatedResponse
        {
            UserName = user.UserName,
            Email = user.Email,
            Id = user.Id
        };

    }
}