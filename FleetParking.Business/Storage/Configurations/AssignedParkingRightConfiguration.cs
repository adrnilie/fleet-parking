using FleetParking.Business.Storage.Entities.AssignedParkingRights;
using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.ParkingRights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetParking.Business.Storage.Configurations;

internal sealed class AssignedParkingRightConfiguration : IEntityTypeConfiguration<AssignedParkingRight>
{
    public void Configure(EntityTypeBuilder<AssignedParkingRight> builder)
    {
        builder.HasKey(apr => apr.Id);

        builder.Property(apr => apr.Id).HasConversion(
            assignedParkingRightId => assignedParkingRightId.Value,
            value => new AssignedParkingRightId(value));

        builder.Property(apr => apr.Status).HasConversion(
            status => (int)status,
            value => (AssignedParkingRightStatus)value);

        builder.HasOne<ParkingRight>()
            .WithMany()
            .HasForeignKey(apr => apr.ParkingRightId);

        builder.HasOne<Parker>()
            .WithMany()
            .HasForeignKey(apr => apr.ParkerId);
    }
}