using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetParking.Business.Migrations
{
    /// <inheritdoc />
    public partial class ParkingRight_AddedOwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingRights_AccessDevices_AccessDeviceId",
                table: "ParkingRights");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccessDeviceId",
                table: "ParkingRights",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ParkingRights",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingRights_AccessDevices_AccessDeviceId",
                table: "ParkingRights",
                column: "AccessDeviceId",
                principalTable: "AccessDevices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingRights_AccessDevices_AccessDeviceId",
                table: "ParkingRights");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ParkingRights");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccessDeviceId",
                table: "ParkingRights",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingRights_AccessDevices_AccessDeviceId",
                table: "ParkingRights",
                column: "AccessDeviceId",
                principalTable: "AccessDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
