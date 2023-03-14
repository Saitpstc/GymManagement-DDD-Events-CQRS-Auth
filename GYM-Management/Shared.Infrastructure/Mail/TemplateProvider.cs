namespace Shared.Infrastructure.Mail;

using KAPorg.Shared.Constant;
using KAPorg.Shared.Model.Mail;
using KAPorg.Shared.Service.Mail.Interface;

public class TemplateProvider : IMailTemplateProvider
{

    public string GetTemplateId(MailTemplates? template)
    {
        var templateId = MailConstants.GetTemplateId(template);
        return templateId;
    }
}