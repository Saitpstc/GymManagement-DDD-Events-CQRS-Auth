namespace KAPorg.Shared.Constant;

using System.Collections.Generic;
using Model.Mail;

public static class MailConstants
{

    //todo:add templates before deploy and store templates inside database
    //todo: Open/Closed principle violation
    private const string EmailConfirmation = "d-aa18d5f5834444c8a811f31e664f0a30";
    private const string ApprovalRequestReceived = "d-5027d8ebc0ab41c5a6dadf35619d624f";
    private const string ApprovalRequestApproved = "d-2e198d9be15c48619f1ace13b9d3f537";
    private const string ApprovalRequestRejected = "d-5bbcdfd0ec554943ba2cc16a1f2a1bb3";
    private const string RecoverPassword = "d-179ecdccc5174935835def90dc63b234";
    private const string SuccessfullyChangedPassword = "d-ddf89c67447243fb81800d800ad2a926";
    private const string WorkSpaceInvitation = "d-221228b017474cd98d2df0ceb94dd17c";
    private const string OrganizationStaffInvitation = "d-2f46c5959e4f4b5d95dd9f60eeeb47d5";
    private const string WelcomeToKAPTeam = "d-8776184aeb444350ad11ac6ff11eb81e";
    private static Dictionary<int, string> TemplateId { get; } = new Dictionary<int, string>()
    {
        {
            (int)MailTemplates.EmailConfirmationTemplate, EmailConfirmation
        },
        {
            (int)MailTemplates.ApprovalRequestReceivedTemplate, ApprovalRequestReceived
        },
        {
            (int)MailTemplates.ApprovalRequestApprovedTemplate, ApprovalRequestApproved
        },
        {
            (int)MailTemplates.ApprovalRequestRejectedTemplate, ApprovalRequestRejected
        },
        {
            (int)MailTemplates.RecoverPasswordTemplate, RecoverPassword
        },
        {
            (int)MailTemplates.SuccessfullyChangedPasswordTemplate, SuccessfullyChangedPassword
        },
        {
            (int)MailTemplates.WorkSpaceInvitationTemplate, WorkSpaceInvitation
        },
        {
            (int)MailTemplates.OrganizationStaffInvitationTemplate, OrganizationStaffInvitation
        },
        {
            (int)MailTemplates.WelcomeToKapTeamTeamplate, WelcomeToKAPTeam
        }
    };

    public static string GetTemplateId(MailTemplates? templateName)
    {
        return TemplateId[(int)templateName];
    }
}