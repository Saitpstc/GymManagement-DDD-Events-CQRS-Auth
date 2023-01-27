namespace Shared.Infrastructure;

using Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private IMediator Mediator;

    public UnitOfWork(DbContext context, IMediator mediator)
    {
        this._context = context;
        Mediator = mediator;
    }

    public async Task<int> CommitAsync(
        CancellationToken cancellationToken = default,
        Guid? internalCommandId = null)
    {
        var entries = _context.ChangeTracker.Entries()
                              .Where(e => e.State is EntityState.Added or EntityState.Modified)
                              .OfType<DataStructureBase>()
                              .ToList();

        if (entries.Count() > 1)
        {
            throw new Exception("There should be only one aggregate modification in one transaction");
        }

        var result = await _context.SaveChangesAsync(cancellationToken);

        var domainEvent = entries.First();

        await Mediator.Publish(domainEvent, cancellationToken);

        return result;
    }
}