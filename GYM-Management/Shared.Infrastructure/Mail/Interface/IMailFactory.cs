namespace Shared.Infrastructure.Mail.Interface;

using SendGrid.Helpers.Mail;
using Shared.Infrastructure.Mail.Models;

public interface IMailFactory
{
    SendGridMessage Create(AppMail mail);
}