using FleetParking.Business.Storage.Entities.AccessDevices;
using FleetParking.Business.Storage.Entities.ParkingRights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetParking.Business.Storage.Configurations;

internal sealed class AccessDeviceConfiguration : IEntityTypeConfiguration<AccessDevice>
{
    public void Configure(EntityTypeBuilder<AccessDevice> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasConversion(
            accessDeviceId => accessDeviceId.Value,
            value => new AccessDeviceId(value));

        builder.Property(a => a.CountryCode)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(a => a.EquipmentType)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(a => a.Value)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasOne<ParkingRight>()
            .WithOne()
            .HasForeignKey<AccessDevice>(a => a.ParkingRightId);
    }
}