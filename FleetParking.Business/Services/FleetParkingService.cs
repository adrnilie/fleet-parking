using FleetParking.Business.Storage;
using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;
using FleetParking.Business.Storage.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace FleetParking.Business.Services;

public interface IFleetParkingService
{
    //Task<Parker> LoadOrCreate(OwnerId ownerId, EmailAddress emailAddress, string name = "");
    //Task<ParkingRight?> GetAvailableParkingRight(OwnerId ownerId);
    Task<bool> AssignParkingRight(OwnerId ownerId, EmailAddress emailAddress, string name = "");
    Task<bool> AcceptInvitation(OwnerId ownerId, ParkerId parkerId);
    Task<bool> RevokeParkingRights(OwnerId ownerId, ParkerId parkerId, params ParkingRightId[] parkingRightIds);
    Task<bool> DeleteParker(OwnerId ownerId, ParkerId parkerId);
}

internal sealed class FleetParkingService : IFleetParkingService
{
    private readonly IFleetParkingDbContext _context;

    public FleetParkingService(IFleetParkingDbContext context)
    {
        _context = context;
    }

    private async Task<Parker> LoadOrCreate(OwnerId ownerId, EmailAddress emailAddress, string name = "")
    {
        var parker = await _context.Parkers
            .FirstOrDefaultAsync(p =>
                p.EmailAddress == emailAddress &&
                p.OwnerId == ownerId &&
                !p.Deleted);

        if (parker is null)
        {
            var newParker = Parker.NewParker(name, emailAddress, ownerId);
            _context.Add(newParker);

            return newParker;
        }

        return parker;
    }

    private async Task<ParkingRight?> GetAvailableParkingRight(OwnerId ownerId)
    {
        var parkingRights = await _context
            .ParkingRights
            .Include(pr => pr.AssignedParkingRights)
            .Where(pr => pr.OwnerId == ownerId)
            .ToListAsync()
            .ConfigureAwait(false);

        if (!parkingRights.Any())
        {
            return null;
        }

        var availableParkingRights = parkingRights.Where(pr => pr.IsAvailable).ToList();
        if (!availableParkingRights.Any())
        {
            return null;
        }

        return availableParkingRights.First();
    }

    public async Task<bool> AssignParkingRight(OwnerId ownerId, EmailAddress emailAddress, string name = "")
    {
        var parker = await LoadOrCreate(ownerId, emailAddress, name);
        var availableParkingRight = await GetAvailableParkingRight(ownerId);
        if (availableParkingRight is null)
        {
            return false;
        }

        parker.AssignParkingRight(availableParkingRight);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AcceptInvitation(OwnerId ownerId, ParkerId parkerId)
    {
        var parker = await _context.Parkers
            .Include(p
                => p.AssignedParkingRights
                    .Where(apr
                        => apr.Status != AssignedParkingRightStatus.Revoked))
            .FirstOrDefaultAsync(p
                => p.Id == parkerId && 
                   p.OwnerId == ownerId &&
                   !p.Deleted);

        if (parker is null)
        {
            return false;
        }

        parker.AcceptInvitation();
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RevokeParkingRights(OwnerId ownerId, ParkerId parkerId, params ParkingRightId[] parkingRightIds)
    {
        if (!parkingRightIds.Any())
        {
            return true;
        }

        var parker = await _context.Parkers
            .Include(p
                => p.AssignedParkingRights
                    .Where(apr
                        => apr.Status != AssignedParkingRightStatus.Revoked &&
                           parkingRightIds.Contains(apr.ParkingRightId)))
            .FirstOrDefaultAsync(p
                => p.Id == parkerId &&
                   p.OwnerId == ownerId &&
                   !p.Deleted);

        if (parker is null)
        {
            return false;
        }

        parker.RevokeParkingRights();
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteParker(OwnerId ownerId, ParkerId parkerId)
    {
        var parker = await _context.Parkers
            .Include(p
                => p.AssignedParkingRights
                    .Where(apr
                        => apr.Status != AssignedParkingRightStatus.Revoked))
            .FirstOrDefaultAsync(p
                => p.Id == parkerId &&
                   p.OwnerId == ownerId &&
                   !p.Deleted);

        if (parker is null)
        {
            return false;
        }

        parker.Delete();

        await _context.SaveChangesAsync();

        return true;
    }
}