using FleetParking.Business.Storage.Entities.AccessDevices;
using FleetParking.Business.Storage.Entities.ParkingRights;
using FleetParking.Business.Storage.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetParking.Business.Storage.Configurations;

internal sealed class ParkingRightConfiguration : IEntityTypeConfiguration<ParkingRight>
{
    public void Configure(EntityTypeBuilder<ParkingRight> builder)
    {
        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.Id).HasConversion(
            parkingRightId => parkingRightId.Value,
            value => new ParkingRightId(value));


        builder.Property(pr => pr.OwnerId).HasConversion(
            ownerId => ownerId.Value,
            value => new OwnerId(value));

        builder.HasOne<AccessDevice>()
            .WithOne()
            .HasForeignKey<ParkingRight>(pr => pr.AccessDeviceId);

        builder.HasMany(pr => pr.AssignedParkingRights)
            .WithOne()
            .HasForeignKey(apr => apr.ParkingRightId)
            .IsRequired();
    }
}