namespace Authorization_Authentication.Application.User.Command;

using FluentValidation;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Application.Contracts;
using Shared.Core.Exceptions;

public class ConfirmEmailCommand:ICommand<string>
{
    public string Code { get; set; }
    public string UserName { get; set; }
}

internal class ConfirmEmailCommandHandler:CommandHandlerBase<ConfirmEmailCommand, string>
{
    private readonly AuthDbContext _context;
    private readonly UserManager<User> _userManager;

    public ConfirmEmailCommandHandler(
        AuthDbContext context,
        UserManager<User> userManager,
        IErrorMessageCollector errorMessageCollector):base(errorMessageCollector)
    {
        _context = context;
        _userManager = userManager;
    }

    public class ConfirmEmailValidator:AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Code).Length(6);
            RuleFor(x => x.Code).Must(x => x.All(char.IsDigit)).WithMessage("Code should contain only numeric values");
        }
    }
    public override async Task<string> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            throw new BusinessLogicException("No user found to confirm email");
        }

        if (user.EmailConfirmed)
        {
            throw new BusinessLogicException("Email already confirmed");
        }
        ConfirmationCode code = _context.ConfirmationCodes.First(x => x.UserId == user.Id);

        if (!(DateTime.Now < code.ValidTo))
        {
            throw new BusinessLogicException("Code is expired");
        }

        if (code.Code != request.Code)
        {
            throw new BusinessLogicException("Incorrect Code");
        }

        user.EmailConfirmed = true;

        _context.ConfirmationCodes.Remove(code);
        await _context.SaveChangesAsync(cancellationToken);

        return "User confirmed";

    }
}