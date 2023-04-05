using FleetParking.Business.Storage.Entities.AccessDevices;
using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.Shared;

namespace FleetParking.Business.Storage.Entities.ParkingRights;

public sealed class ParkingRight
{
    private readonly HashSet<AssignedParkingRight> _assignedParkingRights = new();

    private ParkingRight()
    {

    }

    public ParkingRightId Id { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public AccessDeviceId? AccessDeviceId { get; private set; }
    public OwnerId OwnerId { get; private set; }
    public IReadOnlyCollection<AssignedParkingRight> AssignedParkingRights => _assignedParkingRights.ToList();

    public static ParkingRight NewParkingRight(OwnerId ownerId)
        => new()
        {
            Id = new ParkingRightId(Guid.NewGuid()),
            OwnerId = ownerId,
            StartDate = DateTime.UtcNow,
        };

    public bool IsAvailable
        => !_assignedParkingRights.Any() ||
           _assignedParkingRights.Any(apr => apr.ParkingRightId == Id &&
                                             apr.Status == AssignedParkingRightStatus.Revoked);
}