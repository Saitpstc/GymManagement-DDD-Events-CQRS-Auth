namespace Shared.Infrastructure.Mail.Interface;

using System.Threading.Tasks;
using Shared.Infrastructure.Mail.Models;

public interface IEmailService
{
    Task SendEmailAsync(AppMail model);
    

}