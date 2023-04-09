using FleetParking.Business.Storage.Primitives;

namespace FleetParking.Business.DomainEvents;

public sealed record NewParkerCreated(
    Guid ParkerId,
    Guid OwnerId,
    string EmailAddress,
    string Name
    ) : IDomainEvent;