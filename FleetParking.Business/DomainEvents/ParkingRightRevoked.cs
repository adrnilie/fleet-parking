using FleetParking.Business.Storage.Primitives;

namespace FleetParking.Business.DomainEvents;

public sealed record ParkingRightRevoked(
    Guid ParkingRightId, 
    DateTime RevokedDateUtc) : IDomainEvent;