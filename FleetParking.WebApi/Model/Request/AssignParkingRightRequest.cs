namespace FleetParking.WebApi.Model.Request;

public sealed record AssignParkingRightRequest(string EmailAddress, string Name);