using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pic.Domain;

namespace Pic.Infrastructure;

public class PublishDomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _publisher;

    public PublishDomainEventInterceptor(IMediator publisher)
    {
        _publisher = publisher;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await PublishDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if (dbContext is null)
            return;
        
        var entityWithDomainEvent = dbContext.ChangeTracker.Entries<IHasDomainEvent>()
            .Where(entry => entry.Entity.DomainEvents.Any())
            .Select(entry => entry.Entity)
            .ToList();
        
        var domainEvents = entityWithDomainEvent.SelectMany(entry => entry.DomainEvents).ToList();
        
        entityWithDomainEvent.ForEach(entity => entity.ClearDomainEvents());
        
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
