namespace KAPorg.Shared.Service.Mail.Interface;

using global::Shared.Infrastructure.Mail.Models;
using Model.Mail;
using SendGrid.Helpers.Mail;

public interface IMailFactory
{
    SendGridMessage Create(KapMail mail);
}