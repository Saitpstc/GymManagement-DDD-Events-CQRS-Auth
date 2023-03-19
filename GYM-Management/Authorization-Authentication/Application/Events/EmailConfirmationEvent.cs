namespace Authorization_Authentication.Application.Events;

using MediatR;

public class EmailConfirmationEvent:INotification
{
    public string UserName { get; set; }
}