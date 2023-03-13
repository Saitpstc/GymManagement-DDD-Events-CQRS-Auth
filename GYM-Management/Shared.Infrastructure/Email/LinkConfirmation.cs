namespace Shared.Infrastructure.Email;

using Application.Emails;
using EmailConfirmation;

public class LinkConfirmation:IEmailConfirmationService
{

    public void SendConfirmationEmail(EmailMessage emailMessage)
    {
        
    }

    public void ConfirmEmail()
    {
        throw new NotImplementedException();
    }
}