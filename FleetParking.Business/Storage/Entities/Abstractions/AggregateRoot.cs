using FleetParking.Business.Storage.Primitives;

namespace FleetParking.Business.Storage.Entities.Abstractions;

public abstract class AggregateRoot
{
    private List<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> PendingDomainEvents
    {
        get
        {
            var pendingDomainEvents = _domainEvents;
            _domainEvents = new List<IDomainEvent>();
            return pendingDomainEvents;
        }
    }

    protected void PublishDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}