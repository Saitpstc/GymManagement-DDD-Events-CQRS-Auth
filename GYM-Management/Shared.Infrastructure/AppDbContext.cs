namespace Shared.Infrastructure;

using Core;
using Core.Domain;
using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AppDbContext:DbContext
{
    private readonly IMediator Mediator;


    public AppDbContext(DbContextOptions options, IMediator mediator):base(options)
    {
        Mediator = mediator;

    }
    


    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<DataStructureBase>()
                                   .Where(e => e.State is EntityState.Added or EntityState.Modified)
                                   .ToList();


        var updatedEntries = ChangeTracker.Entries<BaseEntity>()
                                          .Where(e => e.State is EntityState.Modified)
                                          .ToList();

        foreach (var updated in updatedEntries)
        {
            updated.Entity.LastUpdateAt = DateTime.Now;
        }

        var result = await SaveChangesAsync(cancellationToken);
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