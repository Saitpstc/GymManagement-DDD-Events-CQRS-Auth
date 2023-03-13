namespace Shared.Infrastructure.Email.EmailConfirmation;

using Microsoft.Extensions.DependencyInjection;

public interface IEmailConfirmationFactory
{
    IEmailConfirmationService GetService(string _requestConfirmationType);
}

public class EmailConfirmationFactory:IEmailConfirmationFactory
{

    private readonly IEmailConfirmationService _codeConfirmation;
    private readonly IEmailConfirmationService _linkConfirmation;


    public EmailConfirmationFactory(ConfirmationServiceResolver confirmationServiceResolver)
    {
        _codeConfirmation = confirmationServiceResolver("Code") ?? throw new ArgumentNullException(nameof(_codeConfirmation) ,"Error while getting code confirmation service");
        _linkConfirmation = confirmationServiceResolver("Link") ?? throw new ArgumentNullException(nameof(_codeConfirmation), "Error while getting link confirmation service");


    }

    public IEmailConfirmationService GetService(string _requestConfirmationType)
    {
        switch (_requestConfirmationType)
        {
            case "Code": return _codeConfirmation;
            case "Link": return _linkConfirmation;
        }
        ;

        return _codeConfirmation;
    }
}