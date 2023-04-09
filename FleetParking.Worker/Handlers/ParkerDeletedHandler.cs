using FleetParking.Business.DomainEvents;

namespace FleetParking.Worker.Handlers;

public sealed class ParkerDeletedHandler : IHandleMessages<ParkerDeleted>
{
    public Task Handle(ParkerDeleted message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Parker with id {message.ParkerId} deleted.");
        return Task.CompletedTask;
    }
}