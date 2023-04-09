using FleetParking.Business.DomainEvents;
using NServiceBus;

namespace FleetParking.Worker.Handlers;

public sealed class InvitationAcceptedHandler : IHandleMessages<InvitationAccepted>
{
    public Task Handle(InvitationAccepted message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Parker with id {message.ParkerId} accepted invitation.");
        return Task.CompletedTask;
    }
}