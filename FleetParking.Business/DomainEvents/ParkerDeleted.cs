using FleetParking.Business.Storage.Primitives;

namespace FleetParking.Business.DomainEvents;

public sealed record ParkerDeleted(
    Guid ParkerId,
    DateTime DeletionDateUtc) : IDomainEvent;