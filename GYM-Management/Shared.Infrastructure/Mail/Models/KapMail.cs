namespace Shared.Infrastructure.Mail.Models;

using System.Collections.Generic;
using KAPorg.Shared.Model.Mail;

public class KapMail
{
    public KapMailTypes? From { get; set; }
    public MailTemplates? Template { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string PlainTextContent { get; set; }
    public Dictionary<string, string> TemplateData { get; set; }
}