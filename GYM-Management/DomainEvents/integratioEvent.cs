namespace DomainEvents;

using MediatR;

public class integratioEvent:INotification
{
    public Guid CustomerId { get; set; }
    public string UserName { get; set; }
    public string  password { get; set; }
}