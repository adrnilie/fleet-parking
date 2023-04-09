using FleetParking.Business.DomainEvents;

namespace FleetParking.Worker.Handlers;

public sealed class ParkingRightRevokedHandler : IHandleMessages<ParkingRightRevoked>
{
    public Task Handle(ParkingRightRevoked message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Parking right with id {message.ParkingRightId} was revoked");
        return Task.CompletedTask;
    }
}