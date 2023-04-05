using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.ParkingRights;
using FleetParking.Business.Storage.Entities.Shared;

namespace FleetParking.Business.Storage.Entities.Parkers;

public sealed class Parker
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
        => new()
        {
            Id = new ParkerId(Guid.NewGuid()),
            Name = name,
            EmailAddress = emailAddress,
            OwnerId = ownerId,
            CreationDate = DateTime.UtcNow
        };

    public void AssignParkingRight(ParkingRight parkingRight)
    {
        var assignedParkingRight = AssignedParkingRight.Create(Id, parkingRight);
        _assignedParkingRights.Add(assignedParkingRight);
    }

    public void Delete()
    {
        foreach (var assignedParkingRight in _assignedParkingRights)
        {
            assignedParkingRight.Revoke();
        }

        Deleted = true;
    }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
    }
}