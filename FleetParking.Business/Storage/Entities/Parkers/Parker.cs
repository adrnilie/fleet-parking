using FleetParking.Business.DomainEvents;
using FleetParking.Business.Storage.Entities.Abstractions;
using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.ParkingRights;
using FleetParking.Business.Storage.Entities.Shared;

namespace FleetParking.Business.Storage.Entities.Parkers;

public sealed class Parker : AggregateRoot
{
    private readonly HashSet<AssignedParkingRight> _assignedParkingRights = new();

    private Parker()
    {

    }

    public ParkerId Id { get; private set; }

    public string Name { get; private set; }

    public EmailAddress EmailAddress { get; private set; }

    public OwnerId OwnerId { get; private set; }

    public bool Deleted { get; private set; }

    public bool EmailConfirmed { get; private set; }

    public IReadOnlyCollection<AssignedParkingRight> AssignedParkingRights => _assignedParkingRights.ToList();

    public DateTime CreationDate { get; private set; }

    public static Parker NewParker(string name, EmailAddress emailAddress, OwnerId ownerId)
    {
        var parker = new Parker
        {
            Id = new ParkerId(Guid.NewGuid()),
            Name = name,
            EmailAddress = emailAddress,
            OwnerId = ownerId,
            CreationDate = DateTime.UtcNow
        };

        parker.PublishNewParkerCreated();

        return parker;
    }

    public void AssignParkingRight(ParkingRight parkingRight)
    {
        var assignedParkingRight = AssignedParkingRight.Create(Id, parkingRight);
        _assignedParkingRights.Add(assignedParkingRight);

        PublishDomainEvent(new ParkingRightAssigned(
            Id.Value,
            assignedParkingRight.Id.Value,
            parkingRight.Id.Value,
            DateTime.UtcNow));
    }

    public void Delete()
    {
        RevokeParkingRights();
        Deleted = true;

        PublishDomainEvent(new ParkerDeleted(Id.Value, DateTime.UtcNow));
    }

    public void AcceptInvitation()
    {
        EmailConfirmed = true;
        PublishDomainEvent(new InvitationAccepted(Id.Value, DateTime.UtcNow));
    }

    public void RevokeParkingRights()
    {
        foreach (var assignedParkingRight in _assignedParkingRights)
        {
            assignedParkingRight.Revoke();

            PublishDomainEvent(new ParkingRightRevoked(assignedParkingRight.ParkingRightId.Value, DateTime.UtcNow));
        }
    }

    private void PublishNewParkerCreated()
    {
        PublishDomainEvent(new NewParkerCreated(Id.Value, OwnerId.Value, EmailAddress.Value, Name));
    }
}