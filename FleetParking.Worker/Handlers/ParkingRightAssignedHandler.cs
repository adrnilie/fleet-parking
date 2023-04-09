using FleetParking.Business.DomainEvents;

namespace FleetParking.Worker.Handlers;

public sealed class ParkingRightAssignedHandler : IHandleMessages<ParkingRightAssigned>
{
    public Task Handle(ParkingRightAssigned message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Parking right with id {message.ParkingRightId} was assigned to parker with id {message.ParkerId}");
        return Task.CompletedTask;
    }
}