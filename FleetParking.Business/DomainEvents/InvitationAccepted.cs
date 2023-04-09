using FleetParking.Business.Storage.Primitives;

namespace FleetParking.Business.DomainEvents;

public sealed record InvitationAccepted(
    Guid ParkerId,
    DateTime AcceptedDateUtc
) : IDomainEvent;