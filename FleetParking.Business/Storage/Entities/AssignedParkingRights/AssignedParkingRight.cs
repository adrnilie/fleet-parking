using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;

namespace FleetParking.Business.Storage.Entities.AssignedParkingRights;

public sealed class AssignedParkingRight
{
    private AssignedParkingRight()
    {

    }

    public AssignedParkingRightId Id { get; private set; }
    public AssignedParkingRightStatus Status { get; private set; }
    public DateTime CreationDate { get; private set; }

    public ParkingRightId ParkingRightId { get; private set; }
    public ParkerId ParkerId { get; private set; }

    public static AssignedParkingRight Create(ParkerId parkerId, ParkingRight parkingRight)
        => new()
        {
            Id = new AssignedParkingRightId(Guid.NewGuid()),
            CreationDate = DateTime.UtcNow,
            ParkingRightId = parkingRight.Id,
            ParkerId = parkerId,
            Status = AssignedParkingRightStatus.Pending
        };

    public void Revoke()
        => Status = AssignedParkingRightStatus.Revoked;

    public void Accept()
        => Status = AssignedParkingRightStatus.Accepted;
}