namespace Shared.Infrastructure.Mail;

using KAPorg.Shared.Model.Mail;
using KAPorg.Shared.Service.Mail.Interface;
using Models;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;

//todo modify this to make bulk actions
public class MailFactory : IMailFactory
{
    private readonly IMailTemplateProvider _templateProvider;

    public MailFactory(IMailTemplateProvider templateProvider)
    {
        _templateProvider = templateProvider;

    }

    public SendGridMessage Create(KapMail mail)
    {
        try
        {
            return CreateMail(mail);
        }
        catch (SendGridInternalException e)
        {
            throw new SendGridInternalException(e.Message, e.InnerException);
        }
    }


    private SendGridMessage CreateMail(KapMail mail)
    {
        var message = new SendGridMessage();
        message.AddTo(mail.To);
        message.From = new EmailAddress("saitpostaci8@gmail.com");
        if (mail.Template != null)
        {
            message.SetTemplateId(_templateProvider.GetTemplateId(mail.Template));
            message.SetTemplateData(mail.TemplateData);
        }
        else
        {
            message.Subject = mail.Subject;
            message.PlainTextContent = mail.PlainTextContent;
        }
        return message;
    }

}