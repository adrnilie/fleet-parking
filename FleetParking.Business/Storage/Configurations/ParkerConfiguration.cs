using FleetParking.Business.Storage.Entities.Parkers;
using FleetParking.Business.Storage.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetParking.Business.Storage.Configurations;

internal sealed class ParkerConfiguration : IEntityTypeConfiguration<Parker>
{
    public void Configure(EntityTypeBuilder<Parker> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
                parker => parker.Value,
                value => new ParkerId(value));

        builder.Property(p => p.Name)
            .HasMaxLength(200);

        builder.Property(p => p.EmailAddress).HasConversion(
            parker => parker.Value,
            value => new EmailAddress(value));

        builder.Property(p => p.EmailAddress)
            .HasMaxLength(100);

        builder.Property(p => p.OwnerId).HasConversion(
            owner => owner.Value,
            value => new OwnerId(value));

        builder.HasMany(p => p.AssignedParkingRights)
            .WithOne()
            .HasForeignKey(apr => apr.ParkerId);

        builder.HasIndex(p => new { p.EmailAddress, p.OwnerId })
            .IsUnique();
    }
}