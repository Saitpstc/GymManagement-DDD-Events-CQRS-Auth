namespace Shared.Infrastructure.Email.EmailConfirmation;

using Application.Emails;

public interface IEmailConfirmationService
{
    void SendConfirmationEmail(EmailMessage emailMessage);

    void ConfirmEmail();
}