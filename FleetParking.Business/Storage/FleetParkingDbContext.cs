using FleetParking.Business.Storage.Entities.AccessDevices;
using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FleetParking.Business.Storage;

public sealed class FleetParkingDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public FleetParkingDbContext(DbContextOptions<FleetParkingDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Parker> Parkers { get; set; }
    public DbSet<AssignedParkingRight> AssignedParkingRights { get; set; }
    public DbSet<ParkingRight> ParkingRights { get; set; }
    public DbSet<AccessDevice> AccessDevices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_configuration.GetConnectionString("FleetParking") , context =>
        {
            context.MigrationsAssembly(BusinessAssembly.Instance.GetName().Name);

        });
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(BusinessAssembly.Instance);
    }
}