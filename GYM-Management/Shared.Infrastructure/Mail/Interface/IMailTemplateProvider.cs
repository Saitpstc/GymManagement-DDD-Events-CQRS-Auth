namespace KAPorg.Shared.Service.Mail.Interface;

using Model.Mail;

public interface IMailTemplateProvider
{
    string GetTemplateId(MailTemplates? template);
}