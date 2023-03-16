namespace Shared.Infrastructure.Mail;

using Interface;
using Models;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;

//todo modify this to make bulk actions
public class MailFactory:IMailFactory
{

    public SendGridMessage Create(AppMail mail)
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


    private SendGridMessage CreateMail(AppMail mail)
    {
        var message = new SendGridMessage();
        message.AddTo(mail.To);
        message.From = new EmailAddress(mail.From.GetDescription());

        if (mail.Template !=MailTemplates.None)
        {
            message.SetTemplateId(mail.Template.GetDescription());
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