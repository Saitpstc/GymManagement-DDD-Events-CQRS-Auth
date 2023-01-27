namespace CompanyName.MyMeetings.BuildingBlocks.Application.Emails
{
    using Shared.Application.Emails;

    public interface IEmailSender
    {
        void SendEmail(EmailMessage message);
    }
}