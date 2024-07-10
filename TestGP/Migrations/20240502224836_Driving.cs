using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestGP.Migrations
{
    /// <inheritdoc />
    public partial class Driving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Drivers_DriverID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DriverID",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DriverID",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "Drivings",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false),
                    DriverID = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivings", x => new { x.CarID, x.DriverID, x.date });
                    table.ForeignKey(
                        name: "FK_Drivings_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drivings_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivings_DriverID",
                table: "Drivings",
                column: "DriverID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivings");

            migrationBuilder.AddColumn<int>(
                name: "DriverID",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverID",
                table: "Cars",
                column: "DriverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Drivers_DriverID",
                table: "Cars",
                column: "DriverID",
                principalTable: "Drivers",
                principalColumn: "Id");
        }
    }
}
