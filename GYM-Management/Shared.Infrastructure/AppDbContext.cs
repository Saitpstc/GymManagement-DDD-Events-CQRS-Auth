namespace Shared.Infrastructure;

using Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AppDbContext:DbContext, IUnitOfWork
{
    private readonly IMediator Mediator;

    public AppDbContext(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task<int> CommitAsync(
        CancellationToken cancellationToken = default,
        Guid? internalCommandId = null)
    {
        var entries = ChangeTracker.Entries()
                                   .Where(e => e.State is EntityState.Added or EntityState.Modified)
                                   .OfType<DataStructureBase>()
                                   .ToList();

        if (entries.Count() > 1)
        {
            throw new Exception("There should be only one aggregate modification in one transaction");
        }

        var result = await SaveChangesAsync(cancellationToken);

        var domainEvents = entries.First()
                                  .Events;

        foreach (DomainEvent domainEvent in domainEvents)
        {
            await Mediator.Publish(domainEvent, cancellationToken);
        }


        return result;
    }
}