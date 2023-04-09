using FleetParking.Business.Storage.Entities.AccessDevices;
using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.OutboxMessages;
using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FleetParking.Business.Storage;

public interface IFleetParkingDbContext
{
    DbSet<Parker> Parkers { get; set; }
    DbSet<AssignedParkingRight> AssignedParkingRights { get; set; }
    DbSet<ParkingRight> ParkingRights { get; set; }
    DbSet<AccessDevice> AccessDevices { get; set; }
    DbSet<OutboxMessage> OutboxMessages { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    void AddRange(IEnumerable<object> entities);
}