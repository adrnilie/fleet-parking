using FleetParking.Business.Storage.Entities.AccessDevices;
using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.OutboxMessages;
using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;
using FleetParking.Business.Storage.Intercceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FleetParking.Business.Storage;

internal sealed class FleetParkingDbContext : DbContext, IFleetParkingDbContext
{
    private readonly IConfiguration _configuration;
    private readonly ConvertDomainEventsToOutboxMessagesInterceptor _convertDomainEventsToOutboxMessagesInterceptor;

    public FleetParkingDbContext(
        DbContextOptions<FleetParkingDbContext> options, 
        IConfiguration configuration, 
        ConvertDomainEventsToOutboxMessagesInterceptor convertDomainEventsToOutboxMessagesInterceptor)
        : base(options)
    {
        _configuration = configuration;
        _convertDomainEventsToOutboxMessagesInterceptor = convertDomainEventsToOutboxMessagesInterceptor;
    }

    public DbSet<Parker> Parkers { get; set; }
    public DbSet<AssignedParkingRight> AssignedParkingRights { get; set; }
    public DbSet<ParkingRight> ParkingRights { get; set; }
    public DbSet<AccessDevice> AccessDevices { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder
            .UseNpgsql(_configuration.GetConnectionString("FleetParking"), context =>
            {
                context.MigrationsAssembly(BusinessAssembly.Instance.GetName().Name);
            })
            .AddInterceptors(_convertDomainEventsToOutboxMessagesInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(BusinessAssembly.Instance);
    }
}