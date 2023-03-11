namespace Shared.Infrastructure;

using Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UnitOfWork:IUnitOfWork
{
    private readonly IMediator Mediator;
    private readonly DbContext DbContext;

    public UnitOfWork(IMediator mediator, DbContext context)
    {
        Mediator = mediator;
        DbContext = context;
    }


    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        var entries = DbContext.ChangeTracker.Entries<DataStructureBase>()
                               .Where(e => e.State is EntityState.Added or EntityState.Modified)
                               .ToList();


        var updatedEntries = DbContext.ChangeTracker.Entries<BaseEntity>()
                                      .Where(e => e.State is EntityState.Modified)
                                      .ToList();

        foreach (var updated in updatedEntries)
        {
            updated.Entity.LastUpdateAt = DateTime.Now;
        }

        var result = await DbContext.SaveChangesAsync(cancellationToken);
        var events = entries.SelectMany(x => x.Entity.Events).ToList();

        if (events.Any())
        {
            await PublishEvents(cancellationToken, entries.Select(x => x.Entity).ToList());
        }


        return result;

    }


    async private Task PublishEvents(CancellationToken cancellationToken, List<DataStructureBase> entries)
    {

        var domainEvents = entries.First()
                                  .Events;

        foreach (DomainEvent domainEvent in domainEvents)
        {
            await Mediator.Publish(domainEvent, cancellationToken);
        }
    }
}

public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken token);
}