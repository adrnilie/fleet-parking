namespace FleetParking.Contracts;

public sealed record AssignParkingRightRequest(EmailAddress EmailAddress, string Name);