namespace Shared.Infrastructure.Mail.Interface;

using Models;
using SendGrid.Helpers.Mail;

public interface IEmailProvider
{
    EmailAddress GetKapMail(AppMailSender appOrgMailSender);
}