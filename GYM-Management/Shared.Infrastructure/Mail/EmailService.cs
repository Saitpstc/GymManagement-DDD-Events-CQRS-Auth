namespace Shared.Infrastructure.Mail;

using Interface;
using Models;
using SendGrid;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;

//todo:modify to implement bulk mail actions
public class EmailService:IEmailService
{
    private readonly IMailFactory _mailFactory;
    private readonly ISendGridClient _sendGridClient;

    public EmailService(IMailFactory mailFactory, ISendGridClient sendGridClient)
    {
        _mailFactory = mailFactory;
        _sendGridClient = sendGridClient;
    }

    public async Task SendEmailAsync(AppMail model)
    {
        SendGridMessage message = _mailFactory.Create(model);

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

    public async Task SendBulkMail(List<AppMail> mails)
    {
        foreach (AppMail mail in mails)
        {
            SendGridMessage message = _mailFactory.Create(mail);

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