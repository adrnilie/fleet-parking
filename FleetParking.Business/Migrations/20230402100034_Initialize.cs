using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetParking.Business.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    EquipmentType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Value = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ParkingRightId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AccessDeviceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingRights_AccessDevices_AccessDeviceId",
                        column: x => x.AccessDeviceId,
                        principalTable: "AccessDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedParkingRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingRightId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParkerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedParkingRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedParkingRights_Parkers_ParkerId",
                        column: x => x.ParkerId,
                        principalTable: "Parkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedParkingRights_ParkingRights_ParkingRightId",
                        column: x => x.ParkingRightId,
                        principalTable: "ParkingRights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessDevices_ParkingRightId",
                table: "AccessDevices",
                column: "ParkingRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignedParkingRights_ParkerId",
                table: "AssignedParkingRights",
                column: "ParkerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedParkingRights_ParkingRightId",
                table: "AssignedParkingRights",
                column: "ParkingRightId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkers_EmailAddress_OwnerId",
                table: "Parkers",
                columns: new[] { "EmailAddress", "OwnerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingRights_AccessDeviceId",
                table: "ParkingRights",
                column: "AccessDeviceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessDevices_ParkingRights_ParkingRightId",
                table: "AccessDevices",
                column: "ParkingRightId",
                principalTable: "ParkingRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessDevices_ParkingRights_ParkingRightId",
                table: "AccessDevices");

            migrationBuilder.DropTable(
                name: "AssignedParkingRights");

            migrationBuilder.DropTable(
                name: "Parkers");

            migrationBuilder.DropTable(
                name: "ParkingRights");

            migrationBuilder.DropTable(
                name: "AccessDevices");
        }
    }
}
