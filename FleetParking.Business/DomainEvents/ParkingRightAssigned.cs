using FleetParking.Business.Storage.Primitives;

namespace FleetParking.Business.DomainEvents;

public sealed record ParkingRightAssigned(
    Guid ParkerId,
    Guid AssignedParkingRightId,
    Guid ParkingRightId,
    DateTime AssignedDateUtc) : IDomainEvent;