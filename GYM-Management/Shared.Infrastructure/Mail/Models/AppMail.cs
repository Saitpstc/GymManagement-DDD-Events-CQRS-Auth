namespace Shared.Infrastructure.Mail.Models;

public class AppMail
{
    public AppMailSender From { get; set; }
    public MailTemplates Template { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string PlainTextContent { get; set; }
    public Dictionary<string, string> TemplateData { get; set; }
}