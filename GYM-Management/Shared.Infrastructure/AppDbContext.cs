namespace Shared.Infrastructure;

using Core.Domain;
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
        var entries = ChangeTracker.Entries<AggregateRoot>()
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
        var events = entries.SelectMany(x => x.Entity.DomainEvents).ToList();

        if (events.Any())
        {
            await PublishEvents(cancellationToken, entries.Select(x => x.Entity).ToList());
        }


        return result;

    }


    async private Task PublishEvents(CancellationToken cancellationToken, List<AggregateRoot> entries)
    {

        foreach (AggregateRoot VARIABLE in entries)
        {
            foreach (IntegrationEvent domainEvent in VARIABLE.DomainEvents)
            {
                await Mediator.Publish(domainEvent, cancellationToken);
            }
        }


    }
}