namespace Shared.Infrastructure.Mail;

using System.Collections.Generic;
using System.Threading.Tasks;
using KAPorg.Shared.Model.Mail;
using KAPorg.Shared.Service.Mail.Interface;
using Models;
using SendGrid;
using SendGrid.Helpers.Errors.Model;

//todo:modify to implement bulk mail actions
public class EmailService : IEmailService
{
    private readonly IMailFactory _mailFactory;
    private readonly ISendGridClient _sendGridClient;

    public EmailService(IMailFactory mailFactory, ISendGridClient sendGridClient)
    {
        _mailFactory = mailFactory;
        _sendGridClient = sendGridClient;
    }

    public async Task SendEmailAsync(KapMail model)
    {
        var message = _mailFactory.Create(model);
        try
        {
            await _sendGridClient.SendEmailAsync(message);
        }
        catch (SendGridInternalException e)
        {
            //todo log
            throw new SendGridInternalException("something went wrong while sending email");
        }
    }

    public async Task SendBulkMail(List<KapMail> mails)
    {
        foreach (var mail in mails)
        {
            var message = _mailFactory.Create(mail);
            try
            {
                await _sendGridClient.SendEmailAsync(message);
            }
            catch (SendGridInternalException e)
            {
                //todo log
                throw new SendGridInternalException("something went wrong while sending email");
            }
        }
    }
}