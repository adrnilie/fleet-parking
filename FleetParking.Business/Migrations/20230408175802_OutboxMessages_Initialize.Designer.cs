﻿// <auto-generated />
using System;
using FleetParking.Business.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FleetParking.Business.Migrations
{
    [DbContext(typeof(FleetParkingDbContext))]
    [Migration("20230408175802_OutboxMessages_Initialize")]
    partial class OutboxMessages_Initialize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.AccessDevices.AccessDevice", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<string>("EquipmentType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<Guid>("ParkingRightId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("ParkingRightId")
                        .IsUnique();

                    b.ToTable("AccessDevices");
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.AssignedParkingRights.AssignedParkingRight", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ParkerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParkingRightId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkerId");

                    b.HasIndex("ParkingRightId");

                    b.ToTable("AssignedParkingRights");
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.OutboxMessages.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.Parkers.Parker", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress", "OwnerId")
                        .IsUnique();

                    b.ToTable("Parkers");
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.ParkingRights.ParkingRight", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AccessDeviceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccessDeviceId")
                        .IsUnique();

                    b.ToTable("ParkingRights");
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.AccessDevices.AccessDevice", b =>
                {
                    b.HasOne("FleetParking.Business.Storage.Entities.ParkingRights.ParkingRight", null)
                        .WithOne()
                        .HasForeignKey("FleetParking.Business.Storage.Entities.AccessDevices.AccessDevice", "ParkingRightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.AssignedParkingRights.AssignedParkingRight", b =>
                {
                    b.HasOne("FleetParking.Business.Storage.Entities.Parkers.Parker", null)
                        .WithMany("AssignedParkingRights")
                        .HasForeignKey("ParkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetParking.Business.Storage.Entities.ParkingRights.ParkingRight", null)
                        .WithMany("AssignedParkingRights")
                        .HasForeignKey("ParkingRightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.ParkingRights.ParkingRight", b =>
                {
                    b.HasOne("FleetParking.Business.Storage.Entities.AccessDevices.AccessDevice", null)
                        .WithOne()
                        .HasForeignKey("FleetParking.Business.Storage.Entities.ParkingRights.ParkingRight", "AccessDeviceId");
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.Parkers.Parker", b =>
                {
                    b.Navigation("AssignedParkingRights");
                });

            modelBuilder.Entity("FleetParking.Business.Storage.Entities.ParkingRights.ParkingRight", b =>
                {
                    b.Navigation("AssignedParkingRights");
                });
#pragma warning restore 612, 618
        }
    }
}
