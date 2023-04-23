namespace Ledger.Application.Invoice.Handlers;

using Core;
using IntegrationEvents.CustomerModule;
using MediatR;

public class MembershipCreatedHandler:INotificationHandler<MembershipCreatedEvent>
{

    public Task Handle(MembershipCreatedEvent notification, CancellationToken cancellationToken)
    {
        var invoice = new Invoice();
    }
}