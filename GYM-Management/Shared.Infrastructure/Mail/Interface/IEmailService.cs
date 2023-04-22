namespace Shared.Infrastructure.Mail.Interface;

using Models;

public interface IEmailService
{
    Task SendEmailAsync(AppMail model);
}