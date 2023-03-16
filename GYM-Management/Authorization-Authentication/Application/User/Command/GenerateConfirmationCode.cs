namespace Authorization_Authentication.Application.User.Command;

using FluentValidation;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Models;
using Query;
using Shared.Application.Contracts;
using Shared.Core.Exceptions;
using Shared.Infrastructure.Mail.Interface;
using Shared.Infrastructure.Mail.Models;

public class GenerateConfirmationCode:ICommand<string>
{
    public string UserName { get; set; }

}

public class GenerateConfirmationCodeValidator:AbstractValidator<GenerateConfirmationCode>
{
    public GenerateConfirmationCodeValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty");
    }
}
public class GenerateConfirmationCodeHandler:CommandHandlerBase<GenerateConfirmationCode, string>, INotificationHandler<EmailConfirmationEvent>
{
    private readonly IEmailService _emailService;
    private readonly AuthDbContext _context;
    private readonly UserManager<User> _userManager;



    public GenerateConfirmationCodeHandler(
        IEmailService emailService,
        AuthDbContext context,
        IErrorMessageCollector errorMessageCollector,
        UserManager<User> userManager):base(
        errorMessageCollector)
    {
        _emailService = emailService;
        _context = context;
        _userManager = userManager;

    }

    public override async Task<string> Handle(GenerateConfirmationCode request, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            throw new BusinessLogicException($"User is not found with {request.UserName} Username ");
        }

        var generator = new Random();
        var code = generator.Next(0, 1000000).ToString("D6");
        var dateNow = DateTime.Now;
        var confirmationCode = new ConfirmationCode()
        {
            Code = code,
            Created = dateNow,
            ValidTo = dateNow.AddMinutes(2),
            UserId = user.Id
        };

        await _context.ConfirmationCodes.AddAsync(confirmationCode, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var kapmail = new AppMail()
        {
            From = AppMailSender.NoReply,
            Subject = "Account Confirmation Code",
            To = user.Email,
            Template = MailTemplates.EmailConfirmationTemplate,
            TemplateData = new Dictionary<string, string>()
            {
                { "VerificationCode", code },
                { "ReceiverMail", user.Email }
            }
        };
        await _emailService.SendEmailAsync(kapmail);

        return "Confirmation Code has been sent";
    }

    public async Task Handle(EmailConfirmationEvent notification, CancellationToken cancellationToken)
    {
        await Handle(new GenerateConfirmationCode()
        {
            UserName = notification.UserName
        }, cancellationToken);
    }
}