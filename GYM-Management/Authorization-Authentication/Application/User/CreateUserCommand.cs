namespace Authorization_Authentication.Application.User;

using Dto.User;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Application.Contracts;
using Shared.Core.Exceptions;

public record CreateUserCommand:ICommand<UserCreatedResponse>
{
    public string UserName { get; set; }
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

public class CreateUserCommandHandler:CommandHandlerBase<CreateUserCommand, UserCreatedResponse>
{
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(IErrorMessageCollector errorMessageCollector, UserManager<User> userManager):base(errorMessageCollector)
    {
        _userManager = userManager;
    }

    public override async Task<UserCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Email = request.Email,
        };

        if (!string.IsNullOrEmpty(request.UserName))
        {
            user.UserName = request.UserName;
        }

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            throw new BusinessLogicException(result.Errors.Select(x => x.Description).ToList());
        }

        return new UserCreatedResponse()
        {
            UserName = user.UserName,
            Email = user.Email,
            Id = user.Id
        };

    }
}