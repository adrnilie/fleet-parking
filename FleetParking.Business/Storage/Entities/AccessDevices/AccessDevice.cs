using FleetParking.Business.Storage.Entities.ParkingRights;

namespace FleetParking.Business.Storage.Entities.AccessDevices;

public sealed class AccessDevice
{
    private AccessDevice()
    {
        
    }

    public AccessDeviceId Id { get; private set; }
    public string CountryCode { get; private set; }
    public string EquipmentType { get; private set; }
    public string Value { get; private set; }
    public ParkingRightId ParkingRightId { get; private set; }
}