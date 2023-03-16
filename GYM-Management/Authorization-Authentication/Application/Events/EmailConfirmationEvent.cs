namespace Authorization_Authentication.Application.User.Query;

using MediatR;

public class EmailConfirmationEvent:INotification
{
    public string UserName { get; set; }
}