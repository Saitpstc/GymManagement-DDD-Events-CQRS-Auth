namespace KAPorg.Shared.Service.Mail.Interface;

using System.Threading.Tasks;
using global::Shared.Infrastructure.Mail.Models;
using Model.Mail;

public interface IEmailService
{
    Task SendEmailAsync(KapMail model);
    

}