namespace Shared.Infrastructure.Mail.Models;

using System.ComponentModel;

public enum KapMailTypes
{
    [Description("noreply@kaporg.com")]
    KapNoReply,
    [Description("admin@kaporg.com")]
    KapAdmin,
    [Description("info@kaporg.com")]
    KapInfo
}