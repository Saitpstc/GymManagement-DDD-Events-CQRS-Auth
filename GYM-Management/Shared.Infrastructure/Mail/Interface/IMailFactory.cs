namespace Shared.Infrastructure.Mail.Interface;

using Models;
using SendGrid.Helpers.Mail;

public interface IMailFactory
{
    SendGridMessage Create(AppMail mail);
}