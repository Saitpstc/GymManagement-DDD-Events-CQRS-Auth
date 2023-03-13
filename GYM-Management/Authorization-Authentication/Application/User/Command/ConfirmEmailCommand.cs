namespace Authorization_Authentication.Application.User.Command;

using Shared.Application.Contracts;
using Shared.Application.Emails;
using Shared.Infrastructure.Email;
using Shared.Infrastructure.Email.EmailConfirmation;

public class ConfirmEmailCommand:ICommand<string>
{
    public readonly EmailConfirmationTypes ConfirmationType;

    public ConfirmEmailCommand(EmailConfirmationTypes code)
    {
        ConfirmationType = code;
    }

}

public class ConfirmEmailCommandHandler:CommandHandlerBase<ConfirmEmailCommand, string>
{
    private readonly IEmailConfirmationFactory _confirmationFactory;

    public ConfirmEmailCommandHandler(IErrorMessageCollector errorMessageCollector, IEmailConfirmationFactory confirmationFactory):base(
        errorMessageCollector)
    {
        _confirmationFactory = confirmationFactory;
    }

    public override Task<string> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        
        var confirmationService = _confirmationFactory.GetService(request.ConfirmationType.ToString());

        var email = new EmailMessage();


        confirmationService.SendConfirmationEmail(email);

        return Task.FromResult("Confirmation Code has been sent");
    }
}